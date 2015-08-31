using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {

	public static UnitScript[] ourUnitScripts;
	public static PlayerScript[] ourPlayerScripts;
	public static CursorScript ourCursorScript;
	public static GlobalScript ourGlobalScript;
	
	public PlayerScript myMainPlayerScript;

	// Use this for initialization
	void Start () {

		ourGlobalScript = this;

		ourCursorScript = Object.FindObjectOfType<CursorScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ourUnitScripts = Object.FindObjectsOfType<UnitScript>();

		ourPlayerScripts = Object.FindObjectsOfType<PlayerScript>();
	}

	public static void TurnBegin ()
	{
		foreach(PlayerScript playerScript in ourPlayerScripts)
		{
			playerScript.TurnBegin();
		}

		foreach (UnitScript unitScript in ourUnitScripts)
		{
			unitScript.TurnBegin();
		}
	}

	public static void TurnEnd ()
	{
		foreach (PlayerScript playerScript in ourPlayerScripts)
		{
			playerScript.TurnEnd();
		}

		foreach (UnitScript unitScript in ourUnitScripts)
		{
			unitScript.TurnEnd();
		}
	}

}
