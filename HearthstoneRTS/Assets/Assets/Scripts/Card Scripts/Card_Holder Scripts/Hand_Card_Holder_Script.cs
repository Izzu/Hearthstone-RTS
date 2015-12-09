using UnityEngine;
using System.Collections;

public class Hand_Card_Holder_Script : Card_Holder_Script {

	public override Vector3 CardPosition(Card_Animation_FSM_Script input)
	{
		float child = input.transform.GetSiblingIndex();

		float count = myCards.Length;

		if (count < 2)
		{
			return Vector3.zero;
		}

		return new Vector3(Mathf.Lerp(-2f, 2f, child / (count - 1)), 0f, 0f);
	}

	public override Quaternion CardRotation(Card_Animation_FSM_Script input)
	{
		return Quaternion.identity;
	}

	// Update is called once per frame
	void Update () 
	{
		Card_Animation_FSM_Script[] cards = GetComponentsInChildren<Card_Animation_FSM_Script>();

		if (myLastCardCount != cards.Length)
		{
			foreach (Card_Animation_FSM_Script card in cards)
			{
				card.PlayClip(.5f, CardPosition(card), Card_Animation_FSM_Script.CardSize(), CardRotation(card), "reposition");
			}

			myLastCardCount = cards.Length;
		}
	}

	private int myLastCardCount = 0;
}
