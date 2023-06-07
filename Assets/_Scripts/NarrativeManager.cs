using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public GameObject globalVol;
    public GameObject zparticles;

    public static bool starting = true;

    PlayerScript player;

    public RawImage fadeImg;

    bool enablednav = false;

    public VolumeController vol;

    public static NarrativeManager main;

    public GameObject excMark;

    public bool completedsequence = false;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (!SceneController.dontload)
        {
            StartCoroutine(startingSequence());
        }   
        else {
            player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            player.gameObject.GetComponent<Collider>().enabled = true;
            GameObject.Find("z_particles").SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(InventoryManager.main.inventory.Count > 0 && !enablednav)
        {
            enablednav = true;
            player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            player.gameObject.GetComponentInChildren<Animator>().ResetTrigger("Sit");
            player.gameObject.GetComponentInChildren<Animator>().SetTrigger("Idle");
            player.GetComponent<Collider>().enabled = true;
        }
    }

    IEnumerator startingSequence()
    {
        vol.sleepVignette();
        yield return new WaitForEndOfFrame();
        //player.transform.rotation = Quaternion.Euler(0, -90, 0);
        player.frozen = true;
        player.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        player.gameObject.GetComponentInChildren<Animator>().ResetTrigger("Idle");
        player.gameObject.GetComponentInChildren<Animator>().SetTrigger("Sit");
        zparticles.SetActive(false);
        yield return new WaitForSeconds(1f);
        CameraController.main.dontzoom = true;
        WalkingIn.main.go = true;
        while((bool)DialogueManager.main.stories["introChat"].variablesState["completed"] == false || DialogueManager.main.talking == true)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(4.5f);
        CameraController.main.dontzoom = false;
        CameraController.main.talkother = GameObject.Find("Parent");
        DialogueManager.main.startConvo("introParent");
        while(DialogueManager.main.talking)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3f);
        zparticles.SetActive(true);
        yield return new WaitForSeconds(3f);
        StartCoroutine(fadeTo(1, 3f));
        yield return new WaitForSeconds(3f);
        Parentl.main.change();
        deletePeople();
        yield return new WaitForSeconds(1f);
        StartCoroutine(fadeTo(0, 1f));
        yield return new WaitForSeconds(3f);
        zparticles.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        //CameraController.main.talkZoomOut();
        excMark.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        Destroy(excMark);
        DialogueManager.main.startConvo("thoughts");
        while ((bool)DialogueManager.main.stories["thoughts"].variablesState["completed"] == false)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.4f);
        CameraController.main.dontzoom = false;
        vol.wakeup();
        //player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        player.frozen = false;
        starting = false;
        completedsequence = true;
    }

    void deletePeople()
    {
        Destroy(GameObject.FindGameObjectWithTag("People"));
    }

    public IEnumerator fadeTo(float endAlpha, float fadeTime)
    {
        float i = 0;
        Color c = fadeImg.color;
        float startAlpha = c.a;
        while(i < 1)
        {
            i += Time.deltaTime / fadeTime;
            c.a = Mathf.Lerp(startAlpha, endAlpha, i);
            fadeImg.color = c;
            yield return new WaitForEndOfFrame();
        }
    }
}
