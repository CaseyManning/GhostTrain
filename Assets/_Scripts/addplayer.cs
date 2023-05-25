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
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        PlayerScript.player.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
