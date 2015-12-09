using UnityEngine;
using System.Collections;

public class SetResourceEffect : EffectScript
{
	public bool myObject;

	public EffectScript myFunctionScript;

	public enum Enum
	{
		Mana, ManaCap, Overload, Gold, Debt
	}

	public Enum myResource;

	public override float Affect(Message message, float input)
	{
		PlayerScript player = null == message ? null : message.Player(myObject);

		if(null == player)
		{

		}
		else
		{
			switch(myResource)
			{
				case Enum.ManaCap:
					player.myManaCap = (int)myFunctionScript.Affect(message, (float)player.myManaCap);
					break;

				case Enum.Debt:
					player.myDebt = (int)myFunctionScript.Affect(message, (float)player.myDebt);
					break;

				case Enum.Gold:
					player.myGold = (int)myFunctionScript.Affect(message, (float)player.myGold);
					break;

				case Enum.Mana:
					player.myMana = (int)myFunctionScript.Affect(message, (float)player.myMana);
					break;

				case Enum.Overload:
					player.myOverload = (int)myFunctionScript.Affect(message, (float)player.myOverload);
					break;
			}
		}

		return input;
	}
	
}
