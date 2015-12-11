using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class options : MonoBehaviour {

    public Button startbutton;
    public Button optionsbutton;
    public Button exitbutton;
    public Button backbutton;
    public Button choosemap;
    public Button instructions;

    public void OnClick()
    {
        //Debug.Log("clicked");
        startbutton.interactable = false;
        optionsbutton.interactable = false;
        exitbutton.interactable = false;
        backbutton.interactable = true;
        choosemap.interactable = true;
        instructions.interactable = true;
    }
}
