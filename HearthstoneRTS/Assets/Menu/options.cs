using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class options : MonoBehaviour {

    public Button startbutton;

    public void OnClick()
    {
        //Debug.Log("clicked");
        startbutton.interactable = false;
    }
}
