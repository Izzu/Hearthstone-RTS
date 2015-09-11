using UnityEngine;
using System.Collections;

[System.Serializable]
public class OwnerScript : MonoBehaviour
{
	[SerializeField]
	private PlayerScript myOwner;

	[SerializeField]
	private int myDemand, mySupply;

	public PlayerScript owner
	{
		get
		{
			return myOwner;
		}
	}

	void Start ()
	{
		PlayerScript temp = myOwner;

		if(null != myOwner)
		{
			myOwner.Register(this, myDemand, mySupply);
		}
	}

	public int supply
	{
		get
		{
			return mySupply;
		}
	}

	public int demand
	{
		get
		{
			return myDemand;
		}
	}

	public void Own(PlayerScript owner, int demand = 0, int supply = 0)
	{
		if (null != myOwner && myOwner != owner)
		{
			Release();
		}

		myOwner = owner;
		myDemand = demand;
		mySupply = supply;
	}

	private void Release()
	{
		if (null != myOwner)
		{
			myOwner.Release(this);

			myOwner = null;
			mySupply = 0;
			myDemand = 0;
		}
	}

	void OnDestroy()
	{
		Release();
	}
}
