using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using static InventoryManager;
using System;

public class DialogueManager : MonoBehaviour
{
    public GameObject conversationScreen;
    public TMP_Text titleText;
    public TMP_Text bodyText;

    public GameObject option;

    public GameObject teddyBear;

    public bool talking = false;

    public static DialogueManager main;

    List<GameObject> currOptions;

    public List<TextAsset> storyFiles;
    public Dictionary<string, Story> stories;
    public string current;

    public bool firstGhostInteraction = false;

    bool textwriting = false;


    void storyerror(string h, Ink.ErrorType e)
    {
        stopConvo();
        Debug.LogError(h);
    }
    // Start is called before the first frame update
    void Start()
    {
        main = this;
        currOptions = new List<GameObject>();
        stories = new Dictionary<string, Story>();

        for(int i = 0; i < storyFiles.Count; i++)
        {
            Story s = new Story(storyFiles[i].text);
            stories.Add(storyFiles[i].name, s);
            s.onError += new Ink.ErrorHandler(storyerror);
        }
        print("loaded " + storyFiles.Count + " stories.");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startConvo(string pname)
    {
        if(! stories.ContainsKey(pname))
        {
            Debug.LogWarning(" invalid story name " + pname);
        }
        current = pname;
        //if the teddy bear has not been found
        if (pname == "firstGhost")
        {
            firstGhostInteraction = true;
        }
        
        foreach (Item i in InventoryManager.main.inventory)
        {
            if (stories[current].variablesState.GlobalVariableExistsWithName("has" + i.name))
            {
                stories[current].variablesState["has" + i.name] = true;
            }
        }
        if(stories[current].variablesState.GlobalVariableExistsWithName("useplayername"))
        {
            if((bool) stories[current].variablesState["useplayername"] == true)
            {
                stories[current].variablesState["title"] = PlayerScript.character_name.ToUpper();
            }
        }
        print(current);
        print(stories[current].path);
        print(stories[current].variablesState.GlobalVariableExistsWithName("replayable"));
        if (stories[current].canContinue)
        {
            print("trying to continue");
            conversationScreen.SetActive(true);
            CameraController.main.talkzoom();
            talking = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().stopMoving();
            StartCoroutine(updateStory());
        } else if ((bool)stories[current].variablesState["replayable"])
        {
            //stories[current].ResetState();
            stories[current].ResetCallstack();
            stories[current].ChoosePathString("replay");
            conversationScreen.SetActive(true);
            CameraController.main.talkzoom();
            talking = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().stopMoving();
            StartCoroutine(updateStory());
            //} catch (Exception e)
            //{
            //    print(e);
            //}
            }
       
    }

    public IEnumerator updateStory()
    {
        foreach(GameObject g in currOptions)
        {
            Destroy(g);
        }
        titleText.text = (string)stories[current].variablesState["title"];
        currOptions = new List<GameObject>();
        if (textwriting)
        {
            bodyText.text = "";
            foreach (char c in stories[current].Continue())
            {
                bodyText.text += c;
                yield return new WaitForSeconds(0.01f);
            }
        } else
        {
            print(current);
            if (stories[current].canContinue)
            {
                bodyText.text = stories[current].Continue();
            }
        }
        if(stories[current].currentText.Length <= 2) //TODO: shouldnt need this
        {
            if (stories[current].canContinue)
            {
                bodyText.text = stories[current].Continue();
            }
        }
        //print("current body: " + stories[current].currentText);
        bodyText.GetComponent<RectTransform>().ForceUpdateRectTransforms();
        bodyText.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        processTags(stories[current].currentTags);
        
        float height = bodyText.GetComponent<RectTransform>().rect.height;
        float optHeight = 35;

        float start = bodyText.GetComponent<RectTransform>().anchoredPosition.y - height - optHeight;
        float xpos = bodyText.GetComponent<RectTransform>().anchoredPosition.x;
        print("X: " + xpos);
        print("Y: " + start);
        print("height: " + height);
        print(stories[current].currentChoices);
        for (int i = 0; i < stories[current].currentChoices.Count; i++) {
            print("making");
            Choice c = stories[current].currentChoices[i];
            GameObject optionObj = Instantiate(option);
            optionObj.transform.SetParent(conversationScreen.transform);
            optionObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xpos, start - i * optHeight);
            optionObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            optionObj.name = c.index.ToString();
            currOptions.Add(optionObj);
            if (textwriting)
            {
                StartCoroutine(writeText(c.text, optionObj.GetComponent<TMP_Text>()));
            } else
            {
                optionObj.GetComponent<TMP_Text>().text = (i + 1) + ". " + c.text;
            }
            yield return new WaitForSeconds(0.05f);
        }

        if(stories[current].currentChoices.Count == 0)
        {
            GameObject optionObj = Instantiate(option);
            optionObj.transform.SetParent(conversationScreen.transform);
            optionObj.GetComponent<TMP_Text>().text = "Continue";
            optionObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xpos, start);
            optionObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            currOptions.Add(optionObj);
            optionObj.name = "END";
        }
    }

    public void processTags(List<string> tags)
    {
        print(tags.Count);
        foreach (string tag in tags)
        {
            if (tag.StartsWith("gain"))
            {
                InventoryManager.main.pickup(tag.Split(" ")[1]);
                if(tag.IndexOf("glasses") >= 0)
                {
                    StartCoroutine(glassesPopup());
                }
            }
            if (tag.StartsWith("delete"))
            {
                print("eee");
                Destroy(GameObject.Find(tag.Split(" ")[1]));
            }
            if (tag.StartsWith("remove"))
            {
                print("remove what object?");
                InventoryManager.main.dropoff(tag.Split(" ")[1]);
                GameObject.Find(tag.Split(" ")[1]).SetActive(false);
            }
            if(tag.StartsWith("startcooking"))
            {
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
                GameObject.Find("minigame").transform.GetChild(0).gameObject.SetActive(true);
                //CameraController.main.zoomInOnPlayer();
            }
            if (tag.StartsWith("startthrowing"))
            {
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
                GameObject.Find("minigame").transform.GetChild(0).gameObject.SetActive(true);
            }
            // add tag to add to task list
            if(tag.StartsWith("task"))
            {
                ListManager.main.addList(tag.Split(" ")[1]);
            }
            // add tag to complete task
            if(tag.StartsWith("completetask"))
            {
                ListManager.main.removeList(tag.Split(" ")[1]);
            }
        }
    }

    public void stopConvo()
    {
        CameraController.main.talkZoomOut();
        conversationScreen.SetActive(false);
        talking = false;
    }

    public void optionClicked(int index)
    {
        stories[current].ChooseChoiceIndex(index);
        //if (stories[current].canContinue)
        //{
        //    stories[current].Continue();
        //}
        StartCoroutine(updateStory());
    }
    public void continueClicked()
    {
        if (stories[current].canContinue)
        {
            StartCoroutine(updateStory());
        }
        else
        {
            stopConvo();
        }
    }

    IEnumerator writeText(string text, TMP_Text obj)
    {
        obj.text = "";
        foreach (char c in text)
        {
            obj.text += c;
            yield return new WaitForSeconds(0.015f);
        }
    }

    IEnumerator glassesPopup()
    {
        while (DialogueManager.main.talking == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Popup.main.open("Ghost glasses were added to your inventory! You can use inventory items by clicking on their icons on the bottom of the screen.", false);
    }
}
