using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject conversationScreen;
    public TMP_Text titleText;
    public TMP_Text bodyText;

    public GameObject option;

    public bool talking = false;

    public static DialogueManager main;

    List<GameObject> currOptions;

    public List<TextAsset> storyFiles;
    Dictionary<string, Story> stories;
    string current;

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

        if (stories[current].canContinue)
        {
            conversationScreen.SetActive(true);
            CameraController.main.talkzoom();
            talking = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().stopMoving();
            StartCoroutine(updateStory());
        } else if ((bool)stories[current].variablesState["replayable"] == true)
        {
            stories[current].ResetState();
            conversationScreen.SetActive(true);
            CameraController.main.talkzoom();
            talking = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().stopMoving();
            StartCoroutine(updateStory());
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
        //bodyText.text = stories[current].Continue();
        bodyText.text = "";
        foreach (char c in stories[current].Continue())
        {
            bodyText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
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
            //optionObj.GetComponent<TMP_Text>().text = (i+1) + ". " + c.text;
            optionObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xpos, start - i * optHeight);
            optionObj.name = c.index.ToString();
            currOptions.Add(optionObj);
            StartCoroutine(writeText(c.text, optionObj.GetComponent<TMP_Text>()));
            yield return new WaitForSeconds(0.05f);
        }

        if(stories[current].currentChoices.Count == 0)
        {
            GameObject optionObj = Instantiate(option);
            optionObj.transform.SetParent(conversationScreen.transform);
            optionObj.GetComponent<TMP_Text>().text = "Continue";
            optionObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xpos, start - optHeight);
            currOptions.Add(optionObj);
            optionObj.name = "END";
        }
    }

    public void processTags(List<string> tags)
    {
        print(tags.Count);
        foreach(string tag in tags)
        {
            if(tag.StartsWith("gain"))
            {
                InventoryManager.main.pickup(tag.Split(" ")[1]);
            }
            if(tag.StartsWith("delete"))
            {
                print("eee");
                Destroy(GameObject.Find(tag.Split(" ")[1]));
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
        stories[current].Continue();
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
}
