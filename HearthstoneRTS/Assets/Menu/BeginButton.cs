using UnityEngine;
using System.Collections;

public class BeginButton : MonoBehaviour {

    public GUISkin MenuButtons;
    
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        GUIStyle style = GUI.skin.GetStyle("button");

        style.fontSize = (Screen.height + Screen.width) / 50;

        if(GUI.Button(new Rect((Screen.width / 2) - (Screen.width / 32) - (Screen.height / 32), (Screen.height / 3) + (Screen.height / 124) + (Screen.width / 248), (Screen.height + Screen.width) / 15, (Screen.width + Screen.height) / 30), "Begin", "button"))
        {
            print("clicked");
        }
    }
}
