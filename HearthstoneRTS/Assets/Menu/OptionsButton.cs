using UnityEngine;
using System.Collections;

public class OptionsButton : MonoBehaviour {

    public GUISkin MenuButtons;
    
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        if (GUI.Button(new Rect((Screen.width / 2) - (Screen.width / 26) - (Screen.height / 26), (Screen.height / 3) + (Screen.height / 16) + (Screen.width / 32), (Screen.height + Screen.width) / 12, (Screen.width + Screen.height) / 30), "Options", "button"))
        {
            print("clicked");
        }
    }
}
