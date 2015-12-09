using UnityEngine;
using System.Collections;

public class AdditionEffect : EffectScript {

	public float myValue;

	public override float Affect(Message message, float input)
	{
		return input + myValue;
	}

}
