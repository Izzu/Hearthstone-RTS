using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {

	public int myHandIndex;

	public Lerper myPosition = new Lerper();

	private Lerper mySize = new Lerper(new Vector3(.5f, .75f, .01f));

	public Slerper myRotation = new Slerper();

    public HandScript myHandScript;

	private RenderTexture myRenderTexture = new RenderTexture(300, 400, 0);

	private Camera myCamera = new Camera();

	private delegate int myPlayEffect(Message inMessage);
	private delegate int myDiscardEffect(Message inMessage);
	private delegate int myDrawEffect(Message inMessage);

	void Start()
	{
		myRenderTexture.isPowerOfTwo = false;

		myRenderTexture.Create();

		myCamera.targetTexture = myRenderTexture;

		//renderer.material.SetTexture("0", myRenderTexture);
	}

	// Update is called once per frame
	void Update () {

		transform.localRotation = myRotation.Slerp();
		
		transform.localPosition = myPosition.Lerp();

		transform.localScale = mySize.Lerp();
	}
	
	void OnMouseDrag ()
	{
		transform.position = Camera.main.ScreenToWorldPoint(myHandScript.myOwningPlayer.myCurserScript.Cursor());

		myPosition.Reset(transform.localPosition);
	}

	void OnMouseEnter()
	{
		if (myHandScript.myOwningPlayer.myCurserScript.myCardScript != this)
		{
			myRotation.Animate(Quaternion.Euler(0f, 0f, 0f), .2f);

			myPosition.Animate(myHandScript.CardPosition(this) + new Vector3(0f, .5f, -.1f), .2f);

			mySize.Animate(new Vector3(1f, 1.5f, .01f), .2f);
		}
	}

	void OnMouseExit()
	{
		if (myHandScript.myOwningPlayer.myCurserScript.myCardScript != this)
		{
			myRotation.Animate(myHandScript.CardRotation(this), .2f);

			myPosition.Animate(myHandScript.CardPosition(this), .2f);

			mySize.Animate(new Vector3(.5f, .75f, .01f), .2f);
		}
	}

	void OnMouseDown()
	{
		myHandScript.myOwningPlayer.myCurserScript.myCardScript = this;

		myHandScript.myOwningPlayer.myCurserScript.CursorDown(transform);

		mySize.Animate(new Vector3(.25f, .375f, .01f), .2f);

		myRotation.Animate(Quaternion.Euler(0f, 0f, 0f), 0.2f);
	}
 
	void OnMouseUp ()
	{
		myHandScript.myOwningPlayer.myCurserScript.myCardScript = null;

		if (Input.mousePosition.y / Screen.height > .2f)
		{
			mySize.Reset(new Vector3(0f, 0f, 0f));

			return;
		}

		myPosition.Reset(transform.localPosition).Animate(myHandScript.CardPosition(this), .2f);

		myRotation.Reset(transform.localRotation).Animate(myHandScript.CardRotation(this), .2f);

	}

	private int myCostMana = 0;

	public int CostMana
	{
		get { return myCostMana; }
		set { myCostMana = value; }
	}

}
