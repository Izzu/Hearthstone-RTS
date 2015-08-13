using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour {

    public GUISkin MenuButtons;
    public Camera maincamera;
 
    void OnGUI()
    {
        GUI.skin = MenuButtons;

        if(GUI.Button(new Rect(385,410,120,50), "Credits", "button"))
        {
            maincamera.transform.Rotate(Vector3.left * 180f);
        }
    }
}
