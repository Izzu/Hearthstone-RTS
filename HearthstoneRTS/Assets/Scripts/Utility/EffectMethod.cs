using UnityEngine;
using System.Collections;

public delegate int EffectMethod(Message inMessage);

public class EffectMethods {

	public enum Enum {
		Null,
		AddMana
	}

	public static EffectMethod[] methods = 
	{
		Return,
		AddMana
	};

	static int Return(Message input)
	{
		return 0;
	}
	static int Kill(Message input)
	{
		input.mySubject.myUnitScript.Damage(30);

		return 0;
	}

	static int AddMana(Message input)
	{
		input.mySubject.myPlayerScript.AddMana(1);

		return 0;
	}

}
