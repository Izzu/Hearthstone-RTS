using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class back : MonoBehaviour
{
    public string menu;

    public Button startbutton;
    public Button optionsbutton;
    public Button exitbutton;
    public Button backbutton;
    public Button map;
    public Button map1;
    public Button map2;

    public void OnClick()
    {
        //Debug.Log("clicked");
        if(menu == "Options")
        {
            backbutton.interactable = false;
            startbutton.interactable = true;
            optionsbutton.interactable = true;
            exitbutton.interactable = true;
           
        }
        else if(menu == "Maps")
        {
            map1.interactable = false;
            map2.interactable = false;
            map.interactable = true;
            menu = "Options";
        } 
    }

    public void Options()
    {
        menu = "Options";
    }

    public void Maps()
    {
        menu = "Maps";
    }
}
