using UnityEngine;
using System.Collections;

public static class TestConnection
{
	public static bool thereIsConnection = false;

	public static IEnumerator Test ()
	{
		float timeTaken = 0.0F;
		float maxTime = 2.0F;

		while ( true )
		{
			Ping testPing = new Ping( "74.125.79.99" );

			timeTaken = 0.0F;

			while ( !testPing.isDone )
			{
				timeTaken += Time.deltaTime;

				if ( timeTaken > maxTime )
				{
					// if time has exceeded the max
					// time, break out and return false
					thereIsConnection = false;
					break;
				}

				yield return null;
			}
		
			if ( timeTaken <= maxTime ) thereIsConnection = true;
			yield return null;
		}
	}
}