using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class instructions : MonoBehaviour {

    public Button instructionbutton;
    public Text info;
    public Button choosemapbutton;

    public void OnClick()
    {
        //Debug.Log("clicked");
        instructionbutton.interactable = true;
        choosemapbutton.interactable = true;
    }
}
