using UnityEngine;
using System.Collections;

public class Card_Highlighted_State_Script : Finite_State_Script
{
	void Awake()
	{
		myCardAnimationFSM = GetComponent<Card_Animation_FSM_Script>();

		myTransitionStates = new Finite_State_Script[2];
		myTransitionStates[0] = GetComponent<Card_Resting_State_Script>();
		myTransitionStates[1] = GetComponent<Card_Selected_State_Script>();
	}

	public override void Enter()
	{
		myCardAnimationFSM.PlayClip(.5f, myCardAnimationFSM.myCard.myCardHolder.CardPosition(myCardAnimationFSM) + Vector3.up * 0.5f - Vector3.forward * 0.5f, Card_Animation_FSM_Script.CardSize() * 2f, myCardAnimationFSM.myCard.myCardHolder.CardRotation(myCardAnimationFSM), "highlighted");
	}

	void OnMouseExit()
	{
		if(false == enabled)
		{
			return;
		}

		myCardAnimationFSM.CurrentState = myTransitionStates[0];
	}

	void OnMouseDown()
	{
		if (false == enabled)
		{
			return;
		}

		myCardAnimationFSM.CurrentState = myTransitionStates[1];
	}

	private Card_Animation_FSM_Script myCardAnimationFSM;
}
