using UnityEngine;
using System.Collections;

public class Finite_State_Script : MonoBehaviour 
{
	[SerializeField]
	protected Finite_State_Script[] myTransitionStates;

	[SerializeField]
	public string myName;

	void Awake()
	{
		enabled = false;
	}

	public virtual void Leave()
	{
		Debug.Log(myName + " the " + this + " leaves.");
	}

	public virtual void Enter()
	{
		Debug.Log(myName + " the " + this + " enters.");
	}

	//checks to see if the transition is a valid transition for this state machine
	public bool IsValidTransition(Finite_State_Script input)
	{
		foreach (Finite_State_Script state in myTransitionStates)
		{
			if (input == state)
			{
				return true;
			}
		}
		return false;
	}

	void Update()
	{

	}
}
