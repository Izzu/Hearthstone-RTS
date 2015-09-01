using UnityEngine;
using System.Collections;

//	Operations pair delegates with a float
//		<Damage, 4f>
//		<Summon, 3.14f>
//		^ this is the basic idea

//This is a pairing of a delegate and a float value
//Default constructor puts value = 1f
[System.Serializable]
public class Operation
{
	public Enum myEnum = Enum.Null;
	public float myValue = 1f;

	public Operation(Enum input = Enum.Null, float value = 1f)
	{
		myEnum = input;
		myValue = value;
	}

	// Delegate is what all operations call
	// Messages and a float are input into them
	private delegate int Method(Message message, float value = 1f);

	//enums for operations
	public enum Enum
	{
		Null,//Null operations can be recycled
		Value,
		AddMana,
		AddGold,
		ForceBolt,
		Damage,
		NegHandSize
	}

	//Operation table, index with Enums
	private static Method[] ourMethods = 
	{
		Value,//Null has a dummy method, and are replaces in Push()
		Value,
		AddMana,
		AddGold,
		ForceBolt,
		Damage,
		NegHandSize
	};
	
	// Uses the delegate indexed by enum with the float and a message
	public int Activate(Message message)
	{
		return ourMethods[(int)myEnum](message, myValue);
	}

	//	Inserts an operation to an operation array,
	//	If too small [int inch] determines how much to grow by
	//	Handles list = <null> 
	static public Operation[] Push (Operation[] list, Operation input, int inch = 2)
	{
		//	Handle idiots inching wrong
		inch = inch < 1 ? 1 : inch;

		//	Handle null list, i.e. 
		//		"Make me a new list and put this on it"
		if (null == list)
		{
			list = new Operation[2];
			list[0] = input;
			return list;
		}
		else
		{
			//	Find a null indeces to recycle
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i].myEnum == Enum.Null)
				{
					list[i] = input;
					return list;
				}
			}

			//	No vacancies, so make a new list
			Operation[] newList = new Operation[list.Length + 2];

			//	Put old values into new list
			for (int i = 0; i < list.Length; i++)
			{
				newList[i] = list[i];
			}
			
			//	Put input on new list
			newList[list.Length] = input;

			//return new list
			return newList;
		}
	}

	//	For those too lazy to if(null != list) then foreach op in list, op.activate
	//	Handles null list
	static public void ActivateList (Operation[] list, Message input)
	{
		if (null == list)
		{
			return;
		}
		for(int i = 0; i < list.Length; i++)
		{
			list[i].Activate(input);
		}
	}























	/************************************************
	 *												*
	 *												*
	 *			Defined Methods for ops				*
	 *												*
	 *												*
	 ************************************************/

	static int Value(Message message, float value = 1f)
	{
		return (int)value;
	}

	static int ForceBolt(Message message, float value = 1f)
	{


		return 0;
	}

	static int Damage(Message message, float value = 1f)
	{
		UnitScript unitScript = message.myObject.myUnitScript;

		int power = (int)value;

		if (null != unitScript)
		{
			unitScript.Damage(power);
		}

		return 0;
	}

	static int NegHandSize (Message message, float value = 1f)
	{
		PlayerScript playerScript = message.myObject.myPlayerScript;

		if (null != playerScript)
		{
			return - playerScript.myHandScript.CountCards();
		}

		return 0;
	}

	static int AddMana(Message message, float value = 1f)
	{
		PlayerScript playerScript = message.myObject.myPlayerScript;

		int power = (int)value;

		if (null != playerScript)
		{
			playerScript.AddMana(power);
		}
		return 0;
	}

	static int AddGold(Message message, float value = 1f)
	{
		PlayerScript playerScript = message.myObject.myPlayerScript;

		int power = (int)value;

		if (null != playerScript)
		{
			playerScript.AddGold(power);
		}

		return 0;
	}

}
