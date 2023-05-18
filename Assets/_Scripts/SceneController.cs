using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController main;

    public RawImage rawIm;

    int current = 0;

    public static bool dontload = false;

    string[] scenes = { "IntroCar", "TrainCar", "TrainCar_01"};

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        if (!dontload)
        {
            SceneManager.LoadScene(scenes[current], LoadSceneMode.Additive);
            //rawIm.CrossFadeAlpha(0, 0.5f, true);
        }
            StartCoroutine(fadeIn(0.35f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change(bool right)
    {
        if (DialogueManager.main.firstGhostInteraction){
            StartCoroutine(fadeOutIn(0.35f, right));
        }
        //if(right)
        //{
        //    if (current < scenes.Length - 1)
        //    {
        //        current += 1;
        //    }
        //} else
        //{
        //    if(current > 0)
        //    {
        //        current -= 1;
        //        StartCoroutine(fadeOutIn(0.35f, right));
        //    }
        //}
    }

    IEnumerator fadeOutIn(float fadeTime, bool right)
    {
        if((right && current >= scenes.Length - 1) || (!right && current < 1))
        {
            yield break;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<NavMeshAgent>().enabled = false;
        while (rawIm.color.a < 1)
        {
            Color col = rawIm.color;
            col.a += Time.deltaTime / fadeTime;
            rawIm.color = col;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.UnloadSceneAsync(scenes[current]);
        if(right)
        {
            current += 1;
        } else
        {
            current -= 1;
        }
        SceneManager.LoadScene(scenes[current], LoadSceneMode.Additive);

        if (right)
        {
            player.transform.position = new Vector3(3, 0, 0);
        }
        else
        {
            player.transform.position = new Vector3(-3, 0, 0);
        }
        player.GetComponent<NavMeshAgent>().enabled = true;
        player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);

        while (rawIm.color.a > 0)
        {
            Color col = rawIm.color;
            col.a -= Time.deltaTime / fadeTime;
            rawIm.color = col;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator fadeIn(float fadeTime)
    {
        Color col2 = rawIm.color;
        col2.a = 1;
        rawIm.color = col2;
        while (rawIm.color.a > 0)
        {
            Color col = rawIm.color;
            col.a -= Time.deltaTime / fadeTime;
            rawIm.color = col;
            yield return new WaitForEndOfFrame();
        }
    }
}
