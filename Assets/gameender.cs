using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        InventoryManager.main.descpopup.GetComponent<Popup>().open("You finished the game. Thanks for playing!", false);
        StartCoroutine(NarrativeManager.main.fadeTo(1, 2f));
        StartCoroutine(end());
    }

    public IEnumerator end()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
