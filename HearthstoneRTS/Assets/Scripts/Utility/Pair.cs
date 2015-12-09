using UnityEngine;
using System.Collections;

[System.Serializable]
public class Pair<HeadType, TailType> {

	public HeadType myHead;
	public TailType myTail;

	public Pair(HeadType head, TailType tail)
	{
		myHead = head;
		myTail = tail;
	}

	public Pair<HeadType, TailType> Head(HeadType head)
	{
		myHead = head;

		return this;
	}

	public Pair<HeadType, TailType> Tail(TailType tail)
	{
		myTail = tail;

		return this;
	}

}
