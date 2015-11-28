using UnityEngine;
using System.Collections;

public class Deck_Card_Holder_Script : Card_Holder_Script {

	public override Vector3 CardPosition(Card_Animation_FSM_Script input)
	{
		return Vector3.forward * Card_Animation_FSM_Script.CardSize().z * input.transform.GetSiblingIndex();
	}

	public override Quaternion CardRotation(Card_Animation_FSM_Script input)
	{
		return Quaternion.identity;
	}

	// Update is called once per frame
	void Update()
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
