using UnityEngine;
using System.Collections;

public class BeginButton : MonoBehaviour {

    public GUISkin MenuButtons;
    
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        if(GUI.Button(new Rect(395,290,100,50), "Begin", "button"))
        {
            print("clicked");
        }
    }
}
