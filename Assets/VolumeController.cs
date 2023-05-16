using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeController : MonoBehaviour
{
    public static VolumeController main;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnityEngine.Rendering.Universal.ColorAdjustments coloradjust;

    public void ghostmode(bool enter)
    {
        VolumeProfile volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));

        if (!volumeProfile.TryGet(out coloradjust)) throw new System.NullReferenceException(nameof(coloradjust));

        //vignette.intensity.Override(1f);
        //if(enter)
        coloradjust.active = enter;

    }
}
