using UnityEngine;
using System.Collections;

public class TurnScript : MonoBehaviour {

	private float myTurnoverTime = 0f;

	public float myTurnTime = 30f;

	public PlayerScript myCurrentPlayer;

	public PlayerScript Current {
		get { return myCurrentPlayer; }
	}

	public PlayerScript Next
	{
		get {
			
			bool found = false;
			PlayerScript first = null;
			foreach (PlayerScript player in Object.FindObjectsOfType<PlayerScript>())
			{
				if (null == first)
				{
					first = player;
				}
				if (found)
				{
					return player;
				}
				if(player == myCurrentPlayer)
				{
					found = true;
				}
			}
			return first;

		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Time.time > myTurnoverTime)
		{
			myCurrentPlayer = Next;

			myTurnoverTime = Time.time + myTurnTime;
		}

	}

}
