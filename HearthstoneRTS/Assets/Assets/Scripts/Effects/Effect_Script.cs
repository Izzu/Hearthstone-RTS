using UnityEngine;
using System.Collections;

public abstract class Effect_Script : MonoBehaviour 
{
	public abstract int Affect(int input, GameObject inSubject, GameObject inObject);
	
	public static Effect_Script[] Append(Effect_Script[] list, Effect_Script input, int inch = 2)
	{
		//	Handle idiots inching wrong
		inch = inch < 1 ? 1 : inch;

		//	Handle null list, i.e. 
		//		"Make me a new list and put this on it"
		if (null == list)
		{
			list = new Effect_Script[2];
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
			Effect_Script[] newList = new Effect_Script[list.Length + 2];

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

	public static int AffectsList(Effect_Script[] list, int input, GameObject inSubject, GameObject inObject)
	{
		if (null == list)
		{
			return input;
		}

		for (int i = 0; i < list.Length; i++)
		{
			if (null == list[i])
			{
				continue;
			}
			input = list[i].Affect(input, inSubject, inObject);
		}

		return input;
	}

}
