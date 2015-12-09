using UnityEngine;
using System.Collections;

public abstract class Card_Holder_Script : MonoBehaviour {

	public Player_Script myOwner
	{
		get
		{
			return GetComponentInParent<Player_Script>();
		}
	}

	public Card_Script[] myCards
	{
		get
		{
			return GetComponentsInChildren<Card_Script>();
		}
	}

	public abstract Vector3 CardPosition(Card_Animation_FSM_Script input);

	public abstract Quaternion CardRotation(Card_Animation_FSM_Script input);

}
