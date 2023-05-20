using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour
{
    public static VolumeController main;

    Vector2 sleepCenter = new Vector2(0.27f, 0.44f);
    Vector2 normalCenter = new Vector2(0.5f, 0.5f);

    VolumeProfile volumeProfile;

    float intensity = 0.4f;
    float smoothness = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));
        //volumeProfile.try
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnityEngine.Rendering.Universal.ColorAdjustments coloradjust;

    public void ghostmode(bool enter)
    {
        if (!volumeProfile.TryGet(out coloradjust)) throw new System.NullReferenceException(nameof(coloradjust));

        //vignette.intensity.Override(1f);
        //if(enter)
        coloradjust.active = enter;

    }

    public void sleepVignette()
    {
        UnityEngine.Rendering.Universal.Vignette vignette;
        volumeProfile = GetComponent<Volume>()?.profile;

        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

        vignette.center = new Vector2Parameter(new Vector2(0.27f, 0.44f));
    }

    public void wakeup()
    {
        StartCoroutine(waking());
    }

    IEnumerator waking(float duration = 0.5f)
    {
        UnityEngine.Rendering.Universal.Vignette vignette;

        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));

        //vignette.intensity.Override(1f);
        //if(enter)
        vignette.center = new Vector2Parameter(new Vector2(0.27f, 0.44f));

        float i = 0;
        while(i < 1)
        {
            vignette.center = new Vector2Parameter(Vector2.Lerp(sleepCenter, normalCenter, i), true);
            vignette.intensity = new ClampedFloatParameter(Mathf.Lerp(1f, intensity, i), 0, 1, true);
            vignette.intensity = new ClampedFloatParameter(Mathf.Lerp(1f, smoothness, i), 0, 1, true);
            i += Time.deltaTime / duration;
            yield return new WaitForEndOfFrame();
        }
    }
}
