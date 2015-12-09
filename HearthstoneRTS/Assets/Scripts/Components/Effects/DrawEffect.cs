using UnityEngine;
using System.Collections;

public class DrawEffect : EffectScript
{
	public bool myObject;

	public float myValue = 1;

	public override float Affect(Message message, float input)
	{
		PlayerScript player = null == message ? null : message.Player(myObject);

		if (null == player)
		{

		}
		else
		{
			if(myValue > 0)
			{
				for (int i = 0; i < myValue; i++)
				{
					player.Draw(true);
				}
			}
		}

		return input;
	}
}
