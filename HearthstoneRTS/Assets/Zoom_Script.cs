using UnityEngine;
using System.Collections;

public class Zoom_Script : MonoBehaviour 
{
	
	void Update()
	{
		var d = Input.GetAxis("Mouse ScrollWheel");
		if (d > 0f)
		{
			// scroll up
			transform.position = new Vector3(transform.position.x, transform.position.y * 0.95f, transform.position.z);
		}
		else if (d < 0f)
		{
			// scroll down
			transform.position = new Vector3(transform.position.x, transform.position.y * 1.05f, transform.position.z);
		}
	}

}
