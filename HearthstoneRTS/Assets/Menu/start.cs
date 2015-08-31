using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {

    public bool begin;

    void Start()
    {
        if(begin)
            Application.LoadLevel("IslandScene");
    }
}
