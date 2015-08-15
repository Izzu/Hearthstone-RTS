using UnityEngine;
using System.Collections;

public class MenuTitle : MonoBehaviour {

    public GUISkin MenuButtons;

    void OnGUI()
    {
        GUI.skin = MenuButtons;

        GUIStyle style = GUI.skin.GetStyle("label");

        style.fontSize = (Screen.height + Screen.width) / 30;

        GUI.Label(new Rect(5, -10, (Screen.width) - 10, (Screen.height / 2)), "Hearthstone RTS", "label");
    }
}
