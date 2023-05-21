using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introGhost : MonoBehaviour
{
    bool done = false;
    public GameObject teddy;
    // Start is called before the first frame update
    void Start()
    {
        if(NarrativeManager.starting)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(done) { return; }

        if((bool)DialogueManager.main.stories["firstGhost"].variablesState["completedQuest"] == true)
        {
            teddy.SetActive(true);
            done = true;
        }
    }
}
