using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

    public int world = 0;

    public void OnClick()
    {
        /*if (world == 0)
            Application.LoadLevel("IslandScene");
        else if (world == 1)
            Application.LoadLevel("MatchScene");*/

        Application.LoadLevel("MatchScene");
    }
}
