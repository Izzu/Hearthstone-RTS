using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

    public int map = 0;

    public void OnClick()
    {
        Debug.Log(map);
        Application.LoadLevel("MatchScene");
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
