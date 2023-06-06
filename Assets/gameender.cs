using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void entered()
    {
        Popup.main.open("You finished the game!", false);
    }
}
