using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour {

    public GUISkin MenuButtons;
    public Camera maincamera;
 
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        if (GUI.Button(new Rect((Screen.width / 2) - (Screen.width / 29) - (Screen.height / 29), (Screen.height / 3) + (Screen.height / 8) + (Screen.width / 18), (Screen.height + Screen.width) / 13, (Screen.width + Screen.height) / 30), "Credits", "button"))
        {
            maincamera.transform.Rotate(Vector3.left * 180f);
        }
    }
}
