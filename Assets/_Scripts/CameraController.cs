using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public float shakeDuration = 0.5f;

    public float shakeFreq = 5f;
    float timer;

    public float shakeStrength;
    public float shakeSpeed;

    public UniversalRendererData settings;

    bool shaking;

    float randStrength;

    Vector3 orig;

    AudioSource source;

    public static CameraController main;

    public GameObject talkother;

    public float talkZoomLevel = 2f;

    Camera cam;

    Quaternion origRot;
    Quaternion goalRot;

    Vector3 start;
    Vector3 goal;

    float startSize;

    // Start is called before the first frame update
    void Start()
    {
        timer = shakeFreq;
        orig = transform.position;
        source = GetComponent<AudioSource>();

        cam = GetComponent<Camera>();
        startSize = cam.orthographicSize;


        source.Play();
        source.Pause();

        main = this;
    }

    public void talkzoom()
    {
        if(talkother == null)
        {
            return;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 pos = (player.transform.position + talkother.transform.position) / 2f;
        start = transform.position;
        goal = new Vector3(pos.x - 0.8f, transform.position.y, transform.position.z);
        StartCoroutine(zoomIn(1f));
    }

    public void talkZoomOut()
    {
        StartCoroutine(zoomOut(1f));

    }

    public void zoomInOnPlayer()
    {
        start = transform.position;
        goal = PlayerScript.player.transform.position;
        StartCoroutine(zoomIn(1f));
    }

    IEnumerator zoomIn(float time)
    {
        float i = 0;
        while (i < 1)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, startSize / talkZoomLevel, i);
            transform.position = Vector3.Lerp(start, goal, i);
            orig = transform.position;
            i += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator zoomOut(float time)
    {
        float i = 0;
        while (i < 1)
        {
            cam.orthographicSize = Mathf.Lerp(startSize / talkZoomLevel, startSize, i);
            transform.position = Vector3.Lerp(goal, start, i);
            orig = transform.position;
            i += Time.deltaTime / time;
            yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(shaking)
        {
            transform.position = orig + Vector3.up * Mathf.Sin(shakeSpeed * (shakeFreq - timer) * 5 * randStrength) * shakeStrength * randStrength;
            if(timer < 0)
            {
                shaking = false;
                timer = shakeFreq;
                transform.position = orig;
                StartCoroutine(FadeOut(source, 1f));
            }
            return;
        }

        if(timer < 0)
        {
            shaking = true;
            timer = shakeDuration;
            randStrength = Random.value;
            //source.UnPause();
            StartCoroutine(FadeIn(source, 0.15f));
        }

    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.5f * randStrength;
        audioSource.volume = 0;
        audioSource.UnPause();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Pause();
        audioSource.volume = startVolume;
    }
}
