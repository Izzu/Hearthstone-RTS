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

		ourUnitScripts = Object.FindObjectsOfType<UnitScript>();

		ourPlayerScripts = Object.FindObjectsOfType<PlayerScript>();

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
		if (null != ourPlayerScripts)
		{
			foreach (PlayerScript playerScript in ourPlayerScripts)
			{
				playerScript.TurnBegin();
			}
		}
			
		if (null != ourUnitScripts)
		{
			foreach (UnitScript unitScript in ourUnitScripts)
			{
				unitScript.TurnBegin();
			}
		}
	}

	public static void TurnEnd ()
	{
		if (null != ourPlayerScripts)
		{
			foreach (PlayerScript playerScript in ourPlayerScripts)
			{
				playerScript.TurnEnd();
			}
		}

		if (null != ourUnitScripts)
		{
			foreach (UnitScript unitScript in ourUnitScripts)
			{
				unitScript.TurnEnd();
			}
		}
	}

}
