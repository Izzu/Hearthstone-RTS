using UnityEngine;
using System.Collections;

public class Player_Script : MonoBehaviour {

	[SerializeField]
	public Effect_Script[] myManaEffects;
	public int myManaValue;
	public Card_Holder_Script myDeck, myGraveyard, myHand;

	public int mana;

	public int Mana
	{
		get
		{
			return Effect_Script.AffectsList(myManaEffects, myManaValue, gameObject, gameObject);
		}
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		mana = Mana;
	}

}
