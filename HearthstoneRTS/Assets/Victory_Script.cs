using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Victory_Script : MonoBehaviour 
{
	[SerializeField]
	Image myImage;

	void EnableImage()
	{
		myImage.enabled = true;
	}

}
