using UnityEngine;
using System.Collections;

public class DamageEffect : EffectScript
{
	public int myValue;

	public bool myObject;

	[SerializeField]
	Messenger_Script myAttackMessenger;

	public override float Affect(Message message, float input)
	{
		UnitScript unit = null == message ? null : message.Unit(myObject);

		if (null != unit && null != unit.myHealth)
		{
			if (myAttackMessenger)
			{
				myAttackMessenger.Publish();
			}

			unit.myHealth.Damage(myValue);
		}

		return input;
	}
}