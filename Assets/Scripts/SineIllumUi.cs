using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class SineIllumUi : MonoBehaviour
{
	public Gradient gradient;
	public float speed;
	private Image image;

	private void Start()
	{
		image = GetComponent<Image>();
	}

	public void Update()
	{
		float c = 0.5f * Mathf.Sin(Time.time*speed) + .5f;

		image.color = gradient.Evaluate(c);
	}
}