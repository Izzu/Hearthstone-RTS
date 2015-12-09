using UnityEngine;
using System.Collections;

public class Card_Animator_Script : MonoBehaviour 
{

	[SerializeField]
	public 
	CardScript myCard;

	[SerializeField]
	TextMesh mySupplyText, myGoldText, myManaText, myOverText, myDebtText, myNameText;

	void Update()
	{
		myNameText.text = myCard.gameObject.name;

		PlayerScript player = null;

		Transform parent = myCard.transform.parent;
		if (parent)
		{
			HandScript hand = parent.GetComponent<HandScript>();
			if (hand)
			{
				player = hand.myOwningPlayer;
			}
		}

		if(player)
		{
			Message message = player.ToMessage();
			mySupplyText.text = EmptyZero(myCard.mySupply.Cost(message));
			myGoldText.text = EmptyZero(myCard.myGold.Cost(message));
			myManaText.text = EmptyZero(myCard.myMana.Cost(message));
			myOverText.text = EmptyZero(myCard.myOverload.Cost(message));
			myDebtText.text = EmptyZero(myCard.myDebt.Cost(message));
		}
		else
		{
			mySupplyText.text = EmptyZero(myCard.mySupply.myBase);
			myGoldText.text = EmptyZero(myCard.myGold.myBase);
			myManaText.text = EmptyZero(myCard.myMana.myBase);
			myOverText.text = EmptyZero(myCard.myOverload.myBase);
			myDebtText.text = EmptyZero(myCard.myDebt.myBase);
		}
		
	}

	static string EmptyZero(int input)
	{
		return input == 0 ? "" : input.ToString();
	}

	static string EmptyZero(float input)
	{
		return input == 0f ? "" : input.ToString();
	}

}
