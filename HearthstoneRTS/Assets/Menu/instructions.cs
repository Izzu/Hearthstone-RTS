using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class instructions : MonoBehaviour {

    public Button instructionbutton;
    public Button maps;
    public Image info;

    public void OnClick()
    {
        //Debug.Log("clicked");
        instructionbutton.interactable = false;
        maps.interactable = false;
        info.enabled = true;
    }
}
