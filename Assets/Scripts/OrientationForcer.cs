using UnityEngine;
using System.Collections;

public class OrientationForcer : MonoBehaviour
{
	public ScreenOrientation setTo = ScreenOrientation.LandscapeLeft;

	void Awake ()
	{
		Screen.orientation = setTo;
	}
}