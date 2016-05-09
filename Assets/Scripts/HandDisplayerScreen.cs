using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Hand displayer screen.
/// </summary>
public class HandDisplayerScreen : MonoBehaviour 
{
	/// <summary>
	/// The width.
	/// </summary>
	public float width;
	/// <summary>
	/// The display card.
	/// </summary>
	public GameObject displayCard;
	/// <summary>
	/// The cards.
	/// </summary>
	public List<Transform> cards;

	/// <summary>
	/// Sets the cards.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void SetCards (int amount)
	{
		//Debug.Log (amount);

		if (cards.Count > amount)
		{
			while (cards.Count > amount)
			{
				Destroy (cards [0].gameObject);
				cards.RemoveAt (0);
			}
		}
		else if (cards.Count < amount)
		{
			while (cards.Count < amount)
			{
				Transform tempCard = Instantiate (displayCard).GetComponent<Transform> ();
				tempCard.SetParent (this.transform);
				cards.Add (tempCard);
			}
		}

		for (int i = 0; i < cards.Count; i++)
		{
			cards [i].localPosition = new Vector3 (((width / (cards.Count + 1)) * (i + 1)) - (width * 0.5f), 0);
			cards [i].localRotation = Quaternion.Euler(Vector3.zero);
		}
	}
}