using UnityEngine;
using System.Collections;

public class PanningScript : MonoBehaviour {

	private Vector3 myMouseLastPosition;

	public float myPanningFactor = 1.0f;

	public PlayerScript myPlayerScript;

	[SerializeField]
	private float myRightBound, myLeftBound, myForwardBound, myBackwardBound;

	// Update is called once per frame
	void Update () {

		if (false == Input.GetMouseButton(0))
		{
			Panning(Input.mousePosition - myMouseLastPosition);
		}
		
		if(transform.position.x > myRightBound)
		{
			transform.position = new Vector3(myRightBound, transform.position.y, transform.position.z);
		}
		else if(transform.position.x < myLeftBound)
		{
			transform.position = new Vector3(myLeftBound, transform.position.y, transform.position.z);
		}

		if(transform.position.z > myForwardBound)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, myForwardBound);
		}
		else if (transform.position.z < myBackwardBound)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, myBackwardBound);
		}

		myMouseLastPosition = Input.mousePosition;
	}


	void Panning(Vector3 mouseMovement)
	{

		Vector2 screenPosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

		if(Input.GetKey("right"))
		//if (screenPosition.x > 0.975f)
		{
			//moving EAST
			transform.Translate(myPanningFactor * Time.deltaTime, 0.0f, 0.0f);

		}
		else
			if (Input.GetKey("left"))
			//if (screenPosition.x < 0.025f)
		{
			//moving WEST
            transform.Translate(-myPanningFactor * Time.deltaTime, 0.0f, 0.0f);

		}


		if (Input.GetKey("up"))
		//if (screenPosition.y > 0.975f)
		{
			//moving NORTH
            transform.Translate(0.0f, 0.0f, myPanningFactor * Time.deltaTime);

		}
		else
			if (Input.GetKey("down"))
			//if (screenPosition.y < 0.025f)
		{
			//moving SOUTH
            transform.Translate(0.0f, 0.0f, -myPanningFactor * Time.deltaTime);

		}

	}

}
