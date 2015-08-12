using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour {

    public PlayerScript myOwningPlayer;

    public int CountCards()
    {
        return transform.GetComponentsInChildren<CardScript>().Length;
    }
}
