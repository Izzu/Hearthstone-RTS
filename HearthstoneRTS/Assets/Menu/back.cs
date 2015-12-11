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
    public Button instructions;
    public GameObject info;

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
            map.interactable = true;
            map1.interactable = false;
            map2.interactable = false;
            instructions.interactable = true;
            menu = "Options";
        } 
        else if(menu == "Instructions")
        {
            map.interactable = true;
            instructions.interactable = true;
            info.SetActive(false);
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

    public void Instruct()
    {
        menu = "Instructions";
    }
}
