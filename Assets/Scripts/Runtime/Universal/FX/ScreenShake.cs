using UnityEngine;
using System.Collections;

[System.Obsolete("Unused")]
public class ScreenShake : MonoBehaviour
{
	/// <summary>
	/// The shake intensity.
	/// </summary>
    public float shakeIntensity;
	/// <summary>
	/// The shake decrementation.
	/// </summary>
    public float shakeDecrementation;

    public bool positionShake;
    public bool rotationShake;
    public bool lerpDecrement;
    [Range(0,1)] public float lerpSpeed;

    private void Update()
    {
        if (lerpDecrement)
            shakeIntensity = Mathf.Lerp(shakeIntensity, 0, lerpSpeed);
        else
            shakeIntensity = Mathf.Max(shakeIntensity-shakeDecrementation,0);

        if (positionShake)
        {
            float xOff = GetTwoSidedRandom() * shakeIntensity;
            float yOff = GetTwoSidedRandom() * shakeIntensity;
            float zOff = GetTwoSidedRandom() * shakeIntensity;

            transform.localPosition = new Vector3(xOff, yOff, zOff);
        }

        if(rotationShake)
        {
            float xOff = GetTwoSidedRandom() * shakeIntensity;
            float yOff = GetTwoSidedRandom() * shakeIntensity;
            float zOff = GetTwoSidedRandom() * shakeIntensity;

            transform.localRotation = Quaternion.Euler(xOff, yOff, zOff);
        }
    }

    private float GetTwoSidedRandom()
    {
        return (Random.value * 2) - 1;
    }
}