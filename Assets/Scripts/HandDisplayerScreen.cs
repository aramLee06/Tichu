using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandDisplayerScreen : MonoBehaviour 
{
	public float width;
	public GameObject displayCard;
	public List<Transform> cards;

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