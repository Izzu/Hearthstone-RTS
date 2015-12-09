using UnityEngine;
using System.Collections;

public class Assign_Effect_Script : Effect_Script
{

	public int myValue;

	public override int Affect(int input, GameObject inSubject, GameObject inObject)
	{
		return myValue;
	}

}
