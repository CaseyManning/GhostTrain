using System.Collections;
using UnityEngine;
using TMPro;
public class IntroManager : MonoBehaviour
{
    public GameObject introText;

    AudioSource source;

    string[] text = {
        "Eastern Intercapital Express",
        "November 1977"
    };
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(script());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator script()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < text.Length; i++)
        {
            TMP_Text tmp = introText.GetComponent<TMP_Text>();
            tmp.text = "";
            source.Play();
            yield return new WaitForSeconds(0.03f);
            foreach (char c in text[i])
            {
                tmp.text += c;
                yield return new WaitForSeconds(0.025f);
            }
            yield return new WaitForSeconds(0.03f);
            source.Stop();
            yield return new WaitForSeconds(3);
        }
        MenuSceneManager.main.fadeOut();
    }
}
