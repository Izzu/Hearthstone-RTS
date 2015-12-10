using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

    public int map = 1;

    public void OnClick()
    {
        //Debug.Log(map);
        //Application.LoadLevel("MatchScene");
		Application.LoadLevel(map);
    }

    public void Map1()
    {
        map = 1;
    }

    public void Map2()
    {
        map = 2;
    }

}
