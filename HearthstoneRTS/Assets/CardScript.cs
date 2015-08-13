using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {

	public int myHandIndex;
	
	public Lerper myPosition;

    public HandScript myHandScript;

	// Use this for initialization
	void Start () {
		
        myPosition = new Lerper();

	}
	
	// Update is called once per frame
	void Update () {

		transform.localPosition = myPosition.Lerp();
		
	}
	
	void OnMouseDown ()
	{
		myHandScript.myOwningPlayer.myCurserScript.myCardScript = this;

		myHandScript.myOwningPlayer.myCurserScript.CursorDown(transform);
	}
 
	void OnMouseDrag ()
	{
		enabled = false;

		transform.position = Camera.main.ScreenToWorldPoint(myHandScript.myOwningPlayer.myCurserScript.Cursor());
	}

	void OnMouseUp ()
	{
		myHandScript.myOwningPlayer.myCurserScript.myCardScript = null;

		myPosition.Reposition(transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(myHandScript.myOwningPlayer.myCurserScript.Cursor()))).Animate(myHandScript.CardPosition(this), 0.2f);

		enabled = true;
	}
}
