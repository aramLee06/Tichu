using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour 
{
	public MenuCard[] menuCard;
	public byte previousMenu = 0;

	// Return To Main Menu
	// Menu 0
	public void Main ()
	{
		DeactivateButtons ();
		menuCard [0].active = true;
		menuCard [1].active = true;
		menuCard [2].active = true;
		previousMenu = 0;
	}

	// Open Play Menu
	// Card 0
	// Menu 1
	public void Play ()
	{
		Debug.Log ("Play");
		DeactivateButtons ();
		previousMenu = 1;
	}

	// Play_
	// Card 1
	// Menu 2
	public void Play_Singleplayer ()
	{
		Debug.Log ("Play Singleplayer");
		DeactivateButtons ();
		previousMenu = 2;
	}
		
	// Card 2
	// Menu 2
	public void Play_Multiplayer ()
	{
		Debug.Log ("Play Multiplayer");
		DeactivateButtons ();
		menuCard [3].active = true;
		menuCard [4].active = true;
		menuCard [5].active = true;
		previousMenu = 2;
	}

	// Card 3-4-5
	// Menu 3
	public void Select_Multiplayer (int aiAmount = 0)
	{
		Debug.LogFormat ("Play Multiplayer with {0} players and {1} AI.", 4 - aiAmount, aiAmount);
		DeactivateButtons ();
		previousMenu = 3;
	}

	// Open Options Menu
	// Card 6
	// Menu 1
	public void Options ()
	{
		Debug.Log ("Options");
		DeactivateButtons ();
		previousMenu = 1;
	}

	// Options_
	// Card 7
	// Menu 4
	public void Options_Language ()
	{
		Debug.Log ("Language");
		DeactivateButtons ();
		previousMenu = 4;
	}

	// Card 8
	// Menu 4
	public void Options_Volume ()
	{
		Debug.Log ("Volume");
		DeactivateButtons ();
		previousMenu = 4;
	}

	// Open Tutorial
	// Card 9
	// Menu 1
	public void Tutorial ()
	{
		Debug.Log ("Tutorial");
		DeactivateButtons ();
		previousMenu = 1;
	}

	public void Back ()
	{
		switch (previousMenu)
		{
			case 0:
				Debug.Log ("Quit");
				break;
			case 1:
				Main ();
				break;
			case 2:
				Play ();
				break;
			case 3:
				Play_Multiplayer ();
				break;
			case 4:
				Options ();
				break;
			default:
				Main ();
				break;
		}
	}

	public void DeactivateButtons ()
	{
		for (int i = 0; i < menuCard.Length; i++)
		{
			menuCard [i].active = false;
		}
	}
}