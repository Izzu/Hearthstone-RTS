using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resource_View_Script : MonoBehaviour {

	[SerializeField]
	PlayerScript myPlayer;

	[SerializeField]
	Text mySupply, myGold, myMana;

	// Update is called once per frame
	void Update () {

		mySupply.text = myPlayer.myDemand + " / " + myPlayer.mySupply;

		myGold.text = myPlayer.myGold.ToString();
		myMana.text = myPlayer.myMana + " / " + myPlayer.myManaCap;

	}
}
