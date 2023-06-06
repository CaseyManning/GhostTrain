using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class addplayer : MonoBehaviour
{
    private void Awake()
    {
        SceneController.dontload = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Time.time < 20)
        {
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
           // PlayerScript.player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        } else
        {
            SceneController.dontload = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
