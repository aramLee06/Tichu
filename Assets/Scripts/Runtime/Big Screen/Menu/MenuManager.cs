using UnityEngine;
using System.Collections;

/// <summary>
/// Manager/handler for the Menus
/// </summary>
public class MenuManager : Manager
{
	/// <summary>
	/// The singleton.
	/// </summary>
    new public static MenuManager main;

    public override void Awake()
    {
        base.Awake();
        if (!main)
            main = this;
    }

    /// <summary>
    /// Handler for when data is received
    /// </summary>
    /// <param name="data">Received data string</param>
	public override void ReceiveData(string data)
    {
        char[] bytes = data.ToCharArray();

        if(bytes[0] == 'M') //M = Menu Operation
        {
            switch(bytes[1])
            {
                case 'a': //AI Setting
                    {
                        int targetAI = int.Parse(bytes[2].ToString());
                        if (bytes[3] == '+') //Set true
                            GameSettings.enableAI[targetAI] = true;
                        else if (bytes[3] == '-') //Set false
                            GameSettings.enableAI[targetAI] = false;
                    }
                    break;
                case 'd': //Difficulty Setting
                    {
                        int targetAI = int.Parse(bytes[2].ToString());
                        int difficulty = int.Parse(bytes[3].ToString());
                        GameSettings.difficulty[targetAI] = (AIDifficulty) difficulty;
                    }
                    break;
                case 'p': //Progress
                    {
                        //LoadLevel
                    }
                    break;
            }
        }
    }
}