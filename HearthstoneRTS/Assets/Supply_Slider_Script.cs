using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Supply_Slider_Script : MonoBehaviour {

	[SerializeField]
	private PlayerScript myPlayer;

	[SerializeField]
	private Slider mySlider;

	// Update is called once per frame
	void Update () 
	{
		if(myPlayer)
		{
			if (mySlider)
			{
				mySlider.value = myPlayer.myDemand / myPlayer.mySupply;
			}
		}
	}

}
