using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teddyBear : MonoBehaviour
{
    public GameObject bear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("bear not set active " + name);
        //check if the bear puzzle story has been completed 
        if (DialogueManager.main.current == "bearPuzzleComplete")
        {
            Debug.Log("bear set active " + name);
            bear.SetActive(true);
        }
    }
}
