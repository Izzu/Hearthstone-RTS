using UnityEngine;
using System.Collections;

public delegate int EffectMethod(Message inMessage);

public class EffectMethods {

	public enum Enum {
		Null,
		AddMana,
		AddGold,
		ForceBolt,
		Damage
	}

	public static EffectMethod[] methods = 
	{
		Return,
		AddMana,
		AddGold,
		ForceBolt,
		Damage
	};

	static int Return(Message input)
	{
		return 0;
	}

	static int ForceBolt(Message input)
	{


		return 0;
	}

	static int Damage (Message input)
	{
		UnitScript unitScript = input.myObject.myUnitScript;

		int power = (int)input.myPower;

		if (null != unitScript)
		{
			unitScript.Damage(power);
		}

		return 0;
	}

	static int AddMana(Message input)
	{
		PlayerScript playerScript = input.myObject.myPlayerScript;

		int power = (int)input.myPower;

		if (null != playerScript)
		{
			playerScript.AddMana(power);
		}
		return 0;
	}

	static int AddGold(Message input)
	{
		PlayerScript playerScript = input.myObject.myPlayerScript;

		int power = (int)input.myPower;

		if (null != playerScript)
		{
			playerScript.AddGold(power);
		}

		return 0;
	}

	static public Enum[] Push (Enum[] list, Enum input, int inch = 2)
	{
		inch = inch < 1 ? 1 : inch;
		if(null == list)
		{
			list = new Enum[2];
			list[0] = input;
			list[1] = Enum.Null;
			return list;
		}
		else
		{
			for(int i = 0; i < list.Length; i++)
			{
				if(list[i] == Enum.Null)
				{
					list[i] = input;
					return list;
				}
			}

			Enum[] newList = new Enum[list.Length + 2];

			for (int i = 0; i < list.Length; i++ )
			{
				newList[i] = list[i];
			}

			newList[list.Length] = input;
			return newList;
		}
	}

	static public void Affect (Enum[] list, Message input)
	{
		if(null == list)
		{
			return;
		}
		foreach (EffectMethods.Enum effect in list)
		{
			EffectMethods.methods[(int)effect](input);
		}
	}

}
