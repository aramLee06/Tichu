using UnityEngine;
using System.Collections;

/// <summary>
/// Vanish effect.
/// </summary>
public class VanishEffect : MonoBehaviour 
{
	//Example: StartCoroutine (VanishEffect.Vanish(notYourTurnPanel.gameObject, 10));

	/// <summary>
	/// Vanish the specified gameObject using the specified speed.
	/// </summary>
	/// <param name="gameObject">Game object.</param>
	/// <param name="speed">Speed.</param>
	public static IEnumerator Vanish (GameObject gameObject, float speed = 1)
	{
		Transform transform = gameObject.transform;

		while (transform.localScale.x > 0.01f)
		{
			transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (0, transform.localScale.y, transform.localScale.z), Time.deltaTime * speed);

			yield return null;
		}

		transform.localScale = new Vector3 (0, transform.localScale.y, transform.localScale.z);
	}

	/// <summary>
	/// Stretch the specified gameObject.
	/// </summary>
	/// <param name="gameObject">Game object.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="width">Width.</param>
	public static IEnumerator Stretch (GameObject gameObject, float speed = 1, float width = 1)
	{
		Transform transform = gameObject.transform;

		while (transform.localScale.x < width - 0.01f)
		{
			transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (width, transform.localScale.y, transform.localScale.z), Time.deltaTime * speed);

			yield return null;
		}

		transform.localScale = new Vector3 (width, transform.localScale.y, transform.localScale.z);
	}
}