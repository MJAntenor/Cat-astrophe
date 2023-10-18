using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteEditer : MonoBehaviour
{
    public PostProcessVolume cameraAddons;
    private Vignette vignette;
    void Start()
    {
        cameraAddons.profile.TryGetSettings(out vignette);
    }

    void Update()
    {
        if (Duchess.Instance.isHidden == true || Duchess.Instance.isHidden == false)
        {
            vignette.enabled.Override(true);
        }
        else
        {
            vignette.enabled.Override(false);
        }
    }
}
