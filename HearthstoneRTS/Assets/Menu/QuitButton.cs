using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour {

    public GUISkin MenuButtons;
    
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        if(GUI.Button(new Rect(385,470,120,50), "Quit", "button"))
        {
            print("clicked");
        }
    }
}
