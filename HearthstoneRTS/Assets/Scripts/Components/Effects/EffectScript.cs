using UnityEngine;
using System.Collections;

public class EffectScript : MonoBehaviour {

	//	Function for an effect
		//in the case of using this as a function to return some value
		//input is the current value,
		//input = Affect(m, input)
	public virtual float Affect(Message message, float input)
		//input is for mutation purposes, do not use it for ability factors
	{
		Debug.Log("Empty Effect");
		return input;
	}

	public static float AffectsList(EffectScript[] list, Message message, float input = 0f)
	{
		if(null == list)
		{
			return input;
		}

		for(int i = 0; i < list.Length; i ++)
		{
			if(null == list[i])
			{
				continue;
			}	
			input = list[i].Affect(message, input);
		}

		return input;
	}

	public static EffectScript[] Append (EffectScript[] list, EffectScript input, int inch = 2)
	{
		//	Handle idiots inching wrong
		inch = inch < 1 ? 1 : inch;

		//	Handle null list, i.e. 
		//		"Make me a new list and put this on it"
		if (null == list)
		{
			list = new EffectScript[2];
			list[0] = input;
			return list;
		}
		else
		{
			//	Find a null indeces to recycle
			for (int i = 0; i < list.Length; i++)
			{
				if (null == list[i])
				{
					list[i] = input;
					return list;
				}
			}

			//	No vacancies, so make a new list
			EffectScript[] newList = new EffectScript[list.Length + 2];

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

}
