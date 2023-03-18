
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RandomHueShift : MonoBehaviour
{
    private Volume globalVolume;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        globalVolume = GetComponent<Volume>();
        if (globalVolume == null)
        {
            Debug.LogError("Volume component not found on this GameObject!");
            return;
        }

        if (!globalVolume.profile.TryGet(out colorAdjustments))
        {
            Debug.LogError("ColorAdjustments not found in the Volume component!");
            return;
        }

        RandomizeHueShift();
    }

    private void RandomizeHueShift()
    {
        if (colorAdjustments != null)
        {
            float randomHueShift = Random.Range(-180, 180);
            colorAdjustments.hueShift.Override(randomHueShift);
        }
    }
}
