using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager main;

    public Button listButton;

    //public GameObject listSlot;

    public List<Task> taskList;

    Dictionary<string, Task> tasks;

    public GameObject taskListUI;

    private string taskInfo;

    public TMP_Text bodyText;

    //float startPos;

    public Sprite buttonOpen;
    public Sprite buttonClose;


    // Start is called before the first frame update
    void Start()
    {
        //startPos = listSlot.GetComponent<RectTransform>().anchoredPosition.x; 
        taskListUI.SetActive(false);
        main = this;
        listButton.GetComponent<Button>().enabled = true;
        // listButton.GetComponent<Image>().enabled = false;
        taskList = new List<Task>();
        tasks = new Dictionary<string, Task>();
        tasks.Add("teddybear", new Task("teddybear", "Find Teddy Bear", "Find teddy bear and give it back to ghost child."));
        tasks.Add("parents", new Task("parents", "Find Your Parent", "What happened to your parent? Where did they go?"));
        tasks.Add("pancakes", new Task("pancakes", "", "Flip pancake for 6 consecutive times."));
        tasks.Add("fryingpan", new Task("fryingpan", "Find A Frying Pan", "Find a frying pan and give it to Gordon in the kitchen."));  

        // when click on button
        Button button = listButton.GetComponent<Button>();
        button.onClick.AddListener(taskOnClick); 
    }

    public class Task
    {
        public string name;
        public string taskDisplay;
        public string shortDesc;
       //public GameObject obj;
       
        public Task(string name, string shortDesc, string taskDisplay)
        {
            this.name = name;
            this.taskDisplay = taskDisplay;
            this.shortDesc = shortDesc;

        }
    }

    public void writeTaskInfo() {
        Debug.Log(taskList.Count);
        bodyText.text = "";
        if (taskList.Count == 0) {
            bodyText.text = "You have no current tasks.";
        } else {  
            for(int i = 0; i < taskList.Count; i++)
            {
                Task task = taskList[i];
                bodyText.text += (i + 1) + ". " + task.taskDisplay + "\n";
            }
        }
    }

    public Task addTask(string name)
    {
        if(!tasks.ContainsKey(name)) {
            Debug.LogWarning("No such task to add: " + name);
            return null;
        }

        // set button to true initially
        if(taskList.Count == 0) {
            listButton.GetComponent<Button>().enabled = true;
            listButton.GetComponent<Image>().enabled = true;
            Debug.Log("button instantiated");
        }

        Task task = tasks[name];
        //task.obj.transform.SetParent(taskListUI.transform);
        taskList.Add(task);
        writeTaskInfo();
        return task;
    }

    public void removeList(string name)
    {
        if (!tasks.ContainsKey(name))
        {
            Debug.LogWarning("No such task: " + name);
            return;
        }
        Task task = tasks[name];
        taskList.Remove(task);
        writeTaskInfo();
    }

    // when click on button
    public void taskOnClick()
    {
        /*for(int i = 0; i < taskList.Count; i++)
        {
            // how to concatenate lines onto pop-up screen
            popup.GetComponent<Popup>().open((i+1).ToString() + ". " + taskList[i].displayname, true); 
            //popup.GetComponent<Popup>.text += ("\n" + (i+1).ToString + ". " + taskList[i]);
        }*/
        if (taskListUI.activeSelf) {
            taskListUI.SetActive(false);
            listButton.image.sprite = buttonOpen;
            //Debug.Log("task list disabled");
        } else {
            taskListUI.SetActive(true);
            listButton.image.sprite = buttonClose;
            //Debug.Log("task list enabled");
        }
    }

}
