using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingIn : MonoBehaviour
{
    public static WalkingIn main;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    public bool go = false;

    // Update is called once per frame
    void Update()
    {
        if(go)
        {
            if (transform.position.x > 1.3)
            {
                transform.position += Vector3.left * Time.deltaTime * 1.5f;
            } else
            {
                CameraController.main.talkother = gameObject;
                DialogueManager.main.startConvo("introChat");
                go = false;
            }
        }
        if((bool)DialogueManager.main.stories["introChat"].variablesState["completed"] == true)
        {
            Otherguy.go = true;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position += Vector3.right * Time.deltaTime * 1.5f;
        }
    }
}
