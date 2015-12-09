using UnityEngine;
using System.Collections;

public class Card_Decked_State_Script : Finite_State_Script
{
	void Awake()
	{
		myCardAnimationFSM = GetComponent<Card_Animation_FSM_Script>();

		myTransitionStates = new Finite_State_Script[1];
		myTransitionStates[0] = GetComponent<Card_Resting_State_Script>();
	}

	public override void Enter()
	{
		myCardAnimationFSM.PlayClip(.5f, myCardAnimationFSM.myCard.myCardHolder.CardPosition(myCardAnimationFSM), Card_Animation_FSM_Script.CardSize(), myCardAnimationFSM.myCard.myCardHolder.CardRotation(myCardAnimationFSM), "decked");
	}

	void Update()
	{
		Card_Holder_Script holder = myCardAnimationFSM.myCard.myCardHolder;
		if (holder == holder.myOwner.myHand)
		{
			myCardAnimationFSM.CurrentState = myTransitionStates[0];
		}
	}

	private Card_Animation_FSM_Script myCardAnimationFSM;
}
