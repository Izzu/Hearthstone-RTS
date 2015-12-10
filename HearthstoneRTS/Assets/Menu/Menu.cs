using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public int showhide = 0;

	public Button mainmenubutton;
	public Button exitbutton;

	public void OnClick()
	{
		//hide the buttons
		if(showhide == 1)
		{
			mainmenubutton.interactable = false;
			exitbutton.interactable = false;
			showhide = 0;

		}
		//show the buttons
		else if(showhide == 0)
		{
			mainmenubutton.interactable = true;
			exitbutton.interactable = true;
			showhide = 1;
		} 
	}
}
