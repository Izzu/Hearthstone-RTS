using UnityEngine;
using System.Collections;

public class Messenger_Script : MonoBehaviour
{

	[SerializeField]
	private string myName;

	[SerializeField]
	private Subscription[] mySubscribers;

	public void Subscribe(Subscription input)
	{
		Debug.Log(input.Key + " subscribed to " + myName + " the " + this + ".");

		if(null == mySubscribers)
		{
			mySubscribers = new Subscription[1];
			mySubscribers[0] = input;
		}
		else
		{
			var temp = new Subscription[mySubscribers.Length + 1];

			for (int i = 0; i < mySubscribers.Length; i++)
			{
				temp[i] = mySubscribers[i];
			}

			temp[mySubscribers.Length] = input;

			mySubscribers = temp;
		}
	}

	public void Unsubscribe(GameObject input)
	{
		Debug.Log(input + " unsubscribed to " + myName + " the " + this + ".");

		var temp = new Subscription[mySubscribers.Length - 1];

		int j = 0;
		for (int i = 0; i < temp.Length; i++)
		{
			if (mySubscribers[i].Key != input)
			{
				temp[i] = mySubscribers[j++];
			}
		}

		mySubscribers = temp;
	}

	public void Publish()
	{
		if (enabled)
		{
			Debug.Log(myName + " the " + this + " published.");
			
			if (null != mySubscribers)
			{
				foreach (var entry in mySubscribers)
				{
					entry.Key.SendMessage(entry.Value);
				}
			}
		}
	}

	/*public void Publish(object inObject)
	{
		if(enabled)
		{
			Debug.Log(myName + " the " + this + "published: " + inObject);

			foreach(var entry in mySubscribers)
			{
				entry.Key.SendMessage(entry.Value, inObject);
			}
		}
	}*/

	[System.Serializable]
	public class Subscription
	{
		[SerializeField]
		public GameObject Key;

		[SerializeField]
		public string Value;

		public Subscription(GameObject inKey, string inValue)
		{
			Key = inKey;
			Value = inValue;
		}
	}

}
