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
    
        //check if the bear puzzle story has been completed 
        if (DialogueManager.main.current == "bearPuzzleComplete")
        {
            bear.SetActive(true);
        }
    }
}
