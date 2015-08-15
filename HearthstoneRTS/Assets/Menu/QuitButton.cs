using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

    public GUISkin MenuButtons;
    
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        if (GUI.Button(new Rect((Screen.width / 2) - (Screen.width / 40) - (Screen.height / 40), (Screen.height / 3) + (Screen.height / 6) + (Screen.width / 11), (Screen.height + Screen.width) / 20, (Screen.width + Screen.height) / 30), "Quit", "button"))
        {
            print("clicked");
        }
    }
}
