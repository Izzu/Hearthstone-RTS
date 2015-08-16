using UnityEngine;
using System.Collections;

public class PanningScript : MonoBehaviour {

	private Vector3 myMouseLastPosition;

	public float myPanningFactor = 1.0f;

	public PlayerScript myPlayerScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (false == Input.GetMouseButton(0))
		{
			Panning(Input.mousePosition - myMouseLastPosition);
		}

		myMouseLastPosition = Input.mousePosition;

	}


	void Panning(Vector3 mouseMovement)
	{

		Vector2 screenPosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

		if (screenPosition.x > 0.975f)
		{
			//moving EAST
			transform.Translate(myPanningFactor, 0.0f, 0.0f);

		}
		else if (screenPosition.x < 0.025f)
		{
			//moving WEST
			transform.Translate(-myPanningFactor, 0.0f, 0.0f);

		}

		if (screenPosition.y > 0.975f)
		{
			//moving NORTH
			transform.Translate(0.0f, 0.0f, myPanningFactor);

		}
		else if (screenPosition.y < 0.025f)
		{
			//moving SOUTH
			transform.Translate(0.0f, 0.0f, -myPanningFactor);

		}

	}

}
