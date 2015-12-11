using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Victory_Script : MonoBehaviour 
{
	[SerializeField]
	Image myImage;

	[SerializeField]
	Button mainmenubutton;

	[SerializeField]
	Button exitbutton;


	void EnableImage()
	{
		myImage.enabled = true;

		mainmenubutton.interactable = true;
		exitbutton.interactable = true;
	}

}
