using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public RawImage rawIm;
    public static MenuSceneManager main;
    // Start is called before the first frame update
    void Start()
    {
        main = this;
        StartCoroutine(fadeIn(1f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void fadeOut()
    {
        StartCoroutine(fade(1f));
    }

    IEnumerator fade(float fadeTime)
    {
        while (rawIm.color.a < 1)
        {
            Color col = rawIm.color;
            col.a += Time.deltaTime / fadeTime;
            rawIm.color = col;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Player");
    }

    IEnumerator fadeIn(float fadeTime)
    {
        Color col2 = rawIm.color;
        col2.a = 1;
        rawIm.color = col2;
        while (rawIm.color.a > 0)
        {
            print(rawIm.color.a);
            Color col = rawIm.color;
            col.a -= Time.deltaTime / fadeTime;
            rawIm.color = col;
            yield return new WaitForEndOfFrame();
        }
    }
}
