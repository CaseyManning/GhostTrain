using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public GameObject globalVol;

    public bool starting = true;

    PlayerScript player;

    public RawImage fadeImg;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(startingSequence());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator startingSequence()
    {
        yield return new WaitForEndOfFrame();
        player.frozen = true;
        player.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        player.gameObject.GetComponentInChildren<Animator>().ResetTrigger("Idle");
        player.gameObject.GetComponentInChildren<Animator>().SetTrigger("Sit");
        yield return new WaitForSeconds(5);
        //StartCoroutine(fadeTo(0.6f, 1f));
        //yield return new WaitForSeconds(1f);
        //StartCoroutine(fadeTo(0f, 1f));
        //yield return new WaitForSeconds(1f);
        //StartCoroutine(fadeTo(0.6f, 2f));
        //yield return new WaitForSeconds(2f);
        //StartCoroutine(fadeTo(0f, 2f));
        //yield return new WaitForSeconds(2f);
        //StartCoroutine(fadeTo(1f, 1f));
        //yield return new WaitForSeconds(4f);
        //StartCoroutine(fadeTo(0f, 2.5f));
        //yield return new WaitForSeconds(2.5f);
        GameObject.Find("z_particles").SetActive(false);
        player.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        player.frozen = false;
        player.gameObject.GetComponentInChildren<Animator>().ResetTrigger("Sit");
        player.gameObject.GetComponentInChildren<Animator>().SetTrigger("Idle");
        player.GetComponent<Collider>().enabled = true;
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
