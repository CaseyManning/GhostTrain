using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public GameObject globalVol;

    public bool starting = true;

    PlayerScript player;

    public RawImage fadeImg;

    bool enablednav = false;

    public VolumeController vol;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        if (!SceneController.dontload)
        {
            StartCoroutine(startingSequence());
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
        player.frozen = true;
        player.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        player.gameObject.GetComponentInChildren<Animator>().ResetTrigger("Idle");
        player.gameObject.GetComponentInChildren<Animator>().SetTrigger("Sit");
        yield return new WaitForSeconds(5);
        GameObject.Find("z_particles").SetActive(false);
        vol.wakeup();
        //player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        player.frozen = false;
        
    }

    IEnumerator fadeTo(float endAlpha, float fadeTime)
    {
        float i = 0;
        Color c = fadeImg.color;
        float startAlpha = c.a;
        while(i < fadeTime)
        {
            i += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, endAlpha, i);
            fadeImg.color = c;
            yield return new WaitForEndOfFrame();
        }
    }
}
