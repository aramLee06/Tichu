using UnityEngine;
using System.Collections;

public class DogCardEffect : MonoBehaviour
{
    [Range(0,1)]public float lerpSpeed = .2f;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 1.5f, 0), lerpSpeed);
    }
}
