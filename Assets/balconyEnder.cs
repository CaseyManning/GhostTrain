using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class balconyEnder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogueManager.main.stories["finalparent"].canContinue == false && !DialogueManager.main.talking)
        {
            InventoryManager.main.descpopup.GetComponent<Popup>().open("You finished the game. Thanks for playing!", false);
            StartCoroutine(NarrativeManager.main.fadeTo(1, 3f));
            StartCoroutine(end());
        }
    }

    public IEnumerator end()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
