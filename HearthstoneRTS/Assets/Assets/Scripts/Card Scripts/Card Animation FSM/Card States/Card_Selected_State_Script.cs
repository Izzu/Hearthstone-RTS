using UnityEngine;
using System.Collections;

public class Card_Selected_State_Script : Finite_State_Script
{

	void Awake()
	{
		myCardAnimationFSM = GetComponent<Card_Animation_FSM_Script>();

		myTransitionStates = new Finite_State_Script[2];
		myTransitionStates[0] = GetComponent<Card_Highlighted_State_Script>();
		myTransitionStates[1] = GetComponent<Card_Decked_State_Script>();
	}

	void OnMouseDrag()
	{
		if (enabled)
		{
			transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 3.0f);
		}
	}

	void OnMouseUp()
	{
		if (false == enabled)
		{
			return;
		}

		if (Input.mousePosition.y / Screen.height > .2f)
		{
			myCardAnimationFSM.PlayCard(gameObject, gameObject);
			myCardAnimationFSM.CurrentState = myTransitionStates[1];
		}
		else
		{
			myCardAnimationFSM.CurrentState = myTransitionStates[0];
		}
	}

	private Card_Animation_FSM_Script myCardAnimationFSM;

}
