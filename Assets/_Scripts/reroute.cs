using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reroute : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Camera.main == null)
        {
            SceneManager.LoadScene("Player");
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
