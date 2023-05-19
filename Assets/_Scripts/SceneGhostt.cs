using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGhostt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator go()
    {
        gameObject.SetActive(true);
        while(transform.position.x < -0.75f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 3f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.1f);
        CameraController.main.talkother = gameObject;
        DialogueManager.main.startConvo("sceneghost");
    }
}
