using UnityEngine;
using System.Collections;

public class Finite_State_Machine_Script : MonoBehaviour {

	[SerializeField]
	//a reference is an enumeration right?
	protected Finite_State_Script myCurrentState;

	public Finite_State_Script CurrentState
	{
		get
		{
			return myCurrentState;
		}

		set
		{
			Finite_State_Script nextState = value;

			if (myCurrentState.IsValidTransition(nextState))
			{
				myCurrentState.Leave();

				myCurrentState.enabled = false;

				myCurrentState = nextState;

				myCurrentState.enabled = true;

				myCurrentState.Enter();
			}
		}
	}
}
