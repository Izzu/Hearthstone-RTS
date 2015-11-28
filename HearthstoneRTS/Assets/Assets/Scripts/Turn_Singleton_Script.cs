using UnityEngine;
using System.Collections;

public class Turn_Singleton_Script : MonoBehaviour 
{

	public Messenger_Script myEndingMessenger, myBeginningMessenger;

	public static Turn_Singleton_Script Get 
	{
		get
		{
			if (null == ourSingleton)
			{
				Object prefab = Resources.Load("Prefabs/TurnSingleton");

				GameObject gameObject = Instantiate(prefab) as GameObject;

				gameObject.name = prefab.name;

				ourSingleton = gameObject.GetComponent<Turn_Singleton_Script>();
			}

			return ourSingleton;
		}
	}

	public void Turn ()
	{
		myEndingMessenger.Publish();

		myCurrentTurn++;
		
		myBeginningMessenger.Publish();
	}

	public int CurrentTurn
	{
		get
		{
			return myCurrentTurn;
		}
	}

	private static Turn_Singleton_Script ourSingleton;

	private int myCurrentTurn;

}
