using UnityEngine;
using System.Collections;

public class AddResourceEffect : EffectScript
{
	public bool myObject;

	public float myValue = 1;
	
	public enum Enum
	{
		Mana, ManaCap, Overload, Gold, Debt
	}

	public Enum myResource;

	public override float Affect(Message message, float input)
	{
		PlayerScript player = null == message ? null : message.Player(myObject);

		if (null == player)
		{

		}
		else
		{
			switch (myResource)
			{
				case Enum.ManaCap:
					player.AddManaCap((int)myValue);
					break;

				case Enum.Debt:
					player.AddDebt((int)myValue);
					break;

				case Enum.Gold:
					player.AddGold((int)myValue);
					break;

				case Enum.Mana:
					player.AddMana((int)myValue);
					break;

				case Enum.Overload:
					player.AddOverload((int)myValue);
					break;
			}
		}

		return input;
	}

}
