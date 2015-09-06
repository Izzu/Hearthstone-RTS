using UnityEngine;
using System.Collections;

[System.Serializable]
public class Resource
{
	public float myBase;

	public EffectScript[] myEffects;

	public int Cost(Message message)
	{
		if (null == myEffects)
		{
			return (int)myBase;
		}
		else
		{
			return (int)EffectScript.AffectsList(myEffects, message, myBase);
		}
	}
}