using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueOption : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked()
    {
        UISound.main.clickSound();
        if (gameObject.name == "END")
        {
            DialogueManager.main.continueClicked();
        }
        else
        {
            DialogueManager.main.optionClicked(int.Parse(gameObject.name));
        }
    }
}
