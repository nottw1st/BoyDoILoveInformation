using System;
using UnityEngine;

namespace BoyDoILoveInformation.Core;

public class BDILIButton : MonoBehaviour
{
    private const  float  DebounceTime = 0.2f;
    private static float  lastPressTime;
    public         Action OnPress;

    private void Awake() => gameObject.SetLayer(UnityLayer.GorillaInteractable);

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastPressTime < DebounceTime)
            return;

        if (other.GetComponentInParent<GorillaTriggerColliderHandIndicator>() is null)
            return;

        GorillaTriggerColliderHandIndicator handIndicator =
                other.GetComponentInParent<GorillaTriggerColliderHandIndicator>();

        if (handIndicator.isLeftHand)
            return;

        lastPressTime = Time.time;
        OnPress?.Invoke();
        GorillaTagger.Instance.StartVibration(false, 0.3f, 0.15f);
        Plugin.PlaySound(Plugin.BDILIClick);
    }
}