using UnityEngine;
using System.Collections;

/// <summary>
/// An effect for the Dog Card, which would otherwise not be shown.
/// </summary>
public class DogCardEffect : MonoBehaviour
{
	/// <summary>
	/// The lerp speed.
	/// </summary>
    [Range(0,1)]public float lerpSpeed = .2f;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 1.5f, 0), lerpSpeed);
    }
}
