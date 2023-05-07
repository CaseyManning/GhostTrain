using System.Collections;
using UnityEngine;
using TMPro;
public class IntroManager : MonoBehaviour
{
    public GameObject introText;

    string[] text = {
        "something about a kid...",
        "who falls asleep on a train..."
    };
    // Start is called before the first frame update
    void Start()
    {
        introText.GetComponent<TMP_Text>().alpha = 0;
        StartCoroutine(script());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator script()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < text.Length; i++)
        {
            StartCoroutine(fadeText(text[i], 2.5f));
            yield return new WaitForSeconds(5);
        }
        MenuSceneManager.main.fadeOut();
    }

    IEnumerator fadeText(string text, float secs)
    {
        TMP_Text tmp = introText.GetComponent<TMP_Text>();
        tmp.text = text;
        float fadeTime = 0.5f;
        while(tmp.alpha < 1)
        {
            tmp.alpha += Time.deltaTime / fadeTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(secs);

        while (tmp.alpha > 0)
        {
            tmp.alpha -= Time.deltaTime / fadeTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
