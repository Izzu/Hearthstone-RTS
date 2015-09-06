using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {

	public static UnitScript[] ourUnitScripts;
	public static PlayerScript[] ourPlayerScripts;
	public static CursorScript ourCursorScript;
	public static GlobalScript ourGlobalScript;
	public static TurnScript ourTurnScript;
	public static PhaseScript ourPhaseScript;
	/*public static PlayerScript.FrameData ourPlayerFrameData = new PlayerScript.FrameData();
	public static PlayerScript.TurnData ourPlayerTurnData = new PlayerScript.TurnData();
	public static PlayerScript.MatchData ourPlayerMatchData = new PlayerScript.MatchData();*/
	
	public PlayerScript myMainPlayerScript;

	// Use this for initialization
	void Start ()
	{
		ourGlobalScript = this;

		ourUnitScripts = Object.FindObjectsOfType<UnitScript>();

		ourPlayerScripts = Object.FindObjectsOfType<PlayerScript>();

		ourCursorScript = Object.FindObjectOfType<CursorScript>();

		ourTurnScript = Object.FindObjectOfType<TurnScript>();

		ourPhaseScript = Object.FindObjectOfType<PhaseScript>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		ourUnitScripts = Object.FindObjectsOfType<UnitScript>();

		ourPlayerScripts = Object.FindObjectsOfType<PlayerScript>();
	}

	public static void TurnBegin ()
	{
		Debug.Log("Turn: Begin");

		if (null != ourPlayerScripts)
		{
			foreach (PlayerScript playerScript in ourPlayerScripts)
			{
				playerScript.TurnBegin();
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
	}

	static public GameObject New(string path)
	//creates an object and FIXES the name
	{
		Object prefab = Resources.Load(path);

		GameObject gameObject = Instantiate(prefab) as GameObject;

		gameObject.name = prefab.name;

		return gameObject;
	}

	static public GameObject New(string path, Vector3 position, Quaternion rotation)
	//creates an object and FIXES the name
	{
		Object prefab = Resources.Load(path);

		GameObject gameObject = Instantiate(prefab, position, rotation) as GameObject;

		gameObject.name = prefab.name;

		return gameObject;
	}

}
