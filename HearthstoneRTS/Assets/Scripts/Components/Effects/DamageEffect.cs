using UnityEngine;
using System.Collections;

public class DamageEffect : EffectScript
{
	public int myValue;

	public bool myObject;

	public override float Affect(Message message, float input)
	{
		UnitScript unit = null == message ? null : message.Unit(myObject);
		
		if (null != unit && null != unit.myHealth)
		{
			unit.myHealth.Damage(myValue);
		}

		return input;
	}
}