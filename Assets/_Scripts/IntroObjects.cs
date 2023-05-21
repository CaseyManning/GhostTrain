using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!NarrativeManager.starting)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
