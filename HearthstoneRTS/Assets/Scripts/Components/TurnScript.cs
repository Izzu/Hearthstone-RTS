﻿using UnityEngine;
using System.Collections;

public class TurnScript : MonoBehaviour {

	private static Repeater myRepeater = new Repeater(60f);

	private int myLastCycle = 0;

	public PlayerScript myCurrentPlayer;

	public PlayerScript Current {
		get { return myCurrentPlayer; }
	}

	public PlayerScript Next
	{
		get 
		{	
			bool found = false;
			PlayerScript first = null;
			foreach (PlayerScript player in GlobalScript.ourPlayerScripts)
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

		for (; myLastCycle <= myRepeater.Cycle(); myLastCycle++)
		{
			GlobalScript.TurnEnd();

			GlobalScript.TurnBegin();
		}

	}

}
