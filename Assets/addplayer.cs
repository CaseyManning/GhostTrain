using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class addplayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController.dontload = true;
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
