using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnScript : MonoBehaviour {

	public Repeater myRepeater;

	public int myLastCycle = 0;

	public PlayerScript myCurrentPlayer;

	/*public Queue<Pair<EffectScript, Message>> myTurnEndPersistentQueue = new Queue<Pair<EffectScript, Message>>();

	public Queue<Pair<EffectScript, Message>> myTurnBeginPersistentQueue = new Queue<Pair<EffectScript, Message>>();

	public Queue<Pair<EffectScript, Message>> myTurnEndQueue = new Queue<Pair<EffectScript, Message>>();

	public Queue<Pair<EffectScript, Message>> myTurnBeginQueue = new Queue<Pair<EffectScript, Message>>();*/

	public PlayerScript Current {
		get
		{
			return myCurrentPlayer; 
		}
	}

	void Awake()
	{
		myRepeater = new Repeater(60f);
	}

	// Use this for initialization
	void Start()
	{

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

	// Update is called once per frame
	void Update () 
	{
		for (; myLastCycle <= myRepeater.Cycle(); myLastCycle++)
		{
			TurnEnd();

			TurnBegin();
		}
	}

	public void TurnEnd ()
	{
		
		foreach(PlayerScript player in GlobalScript.ourPlayerScripts)
		{
			player.TurnEnd();
		}

		/*foreach (Pair<EffectScript, Message> pair in myTurnEndPersistentQueue)
		{
			pair.myHead.List(pair.myTail);
		}

		foreach (Pair<EffectScript, Message> pair in myTurnEndQueue)
		{
			pair.myHead.Activate(pair.myTail);
		}

		myTurnEndQueue.Clear();*/
	}

	public void TurnBegin()
	{

		Debug.Log("Turn Begins");

		foreach (PlayerScript player in GlobalScript.ourPlayerScripts)
		{
			player.TurnBegin();
		}

		/*foreach (Pair<EffectScript, Message> pair in myTurnBeginPersistentQueue)
		{
			pair.myHead.Activate(pair.myTail);
		}

		foreach (Pair<EffectScript, Message> pair in myTurnBeginQueue)
		{
			pair.myHead.Activate(pair.myTail);
		}

		myTurnBeginQueue.Clear();*/
	}
}
