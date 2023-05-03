using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float shakeDuration = 0.5f;

    public float shakeFreq = 5f;
    float timer;

    public float shakeStrength;
    public float shakeSpeed;

    bool shaking;

    float randStrength;

    Vector3 orig;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        timer = shakeFreq;
        orig = transform.position;
        source = GetComponent<AudioSource>();

        source.Play();
        source.Pause();
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
