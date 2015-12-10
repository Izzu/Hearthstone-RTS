using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class choosemap : MonoBehaviour {

    public Button back;
    public Button map;
    public Button map1;
    public Button map2;
	public Button map3;

    public void OnClick()
    {
        //Debug.Log("clicked");
        map.interactable = false;
        map1.interactable = true;
        map2.interactable = true;
		map3.interactable = true;
    }
}
