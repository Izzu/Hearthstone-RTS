using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {

	public static UnitScript[] ourUnitScripts;
	public static PlayerScript[] ourPlayerScripts;

	public enum Phase {
		Null,
		Draw,
		Battle
	}

	public static Phase ourPhase = Phase.Null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		ourUnitScripts = Object.FindObjectsOfType<UnitScript>();

		ourPlayerScripts = Object.FindObjectsOfType<PlayerScript>();

		switch(ourPhase)
		{
			case Phase.Null:

				break;

			case Phase.Draw:

				break;

			case Phase.Battle:

				break;
		}

	}
}
