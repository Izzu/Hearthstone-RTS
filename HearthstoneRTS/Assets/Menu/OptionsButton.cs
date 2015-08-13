using UnityEngine;
using System.Collections;

public class OptionsButton : MonoBehaviour {

    public GUISkin MenuButtons;
    
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        if(GUI.Button(new Rect(385,350,120,50), "Options", "button"))
        {
            print("clicked");
        }
    }
}
