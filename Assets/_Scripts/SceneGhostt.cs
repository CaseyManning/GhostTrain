using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGhostt : MonoBehaviour
{

    public bool went = false;
    public bool left = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(went && !DialogueManager.main.talking && !left)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().frozen = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().nav.enabled = true;
            VolumeController.main.ghostmode(false);

            StartCoroutine(leave());
            left = true;
        }
    }

    public IEnumerator go()
    {
        gameObject.SetActive(true);
        while(transform.position.x < -0f)
        {
            transform.Translate(Vector3.right * Time.deltaTime * 2f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.1f);
        CameraController.main.talkother = gameObject;
        went = true;
        DialogueManager.main.startConvo("sceneghost");
    }

    public IEnumerator leave()
    {
        while (transform.position.x > -4f)
        {
            transform.Translate(Vector3.left * Time.deltaTime * 2f);
            yield return new WaitForEndOfFrame();
        }
    }
}
