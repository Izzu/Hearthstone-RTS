using UnityEngine;
using System.Collections;

public class Phaser {

	public float begin
	{
		get
		{
			return myClocks[0];
		}
	}

	public float end
	{
		get
		{
			return myClocks[myClocks.Length - 1];
		}
	}

	public bool isActive
	{
		get
		{
			return (begin < end) & (Time.time < end);
		}
	}

	public float percent
	{
		get
		{
			return (Time.time - begin) / (end - begin);
		}
	}

	public int phase
	{
		get
		{
			float time = Time.time;
			if(time < begin)
			{
				return -1;
			}
			else if (time > end)
			{
				return myClocks.Length - 1;
			}

			//binary search for which is correct
			int hi = myClocks.Length - 1;
			
			int lo = 0;

			int i = (hi + lo) >> 1;
			
			for (; 1 != hi - lo; i = (hi + lo) >> 1)
			{
				if(time < myClocks[i])
				{
					hi = i;
				}
				else
				{
					lo = i;
				}
			}
			return i - 1;
		}
	}

	public float phasePercent
		//before the first phase is 0
		//after the last is 1
	{
		get
		{
			int p = phase;

			if(-1 == p)
			{
				return 0f;
			}
			if(myClocks.Length - 1 == p)
			{
				return 1f;
			}

			float start = myClocks[p];
			float finish = myClocks[p + 1];

			return (Time.time - start) / (finish - start);
		}
	}

	public Phaser(float[] durations)
	{
		if(null == durations)
		{
			throw new System.ArgumentException("Parameter cannot be null", "durations");
		}

		float time = Time.time;

		myClocks = new float[durations.Length + 1];

		myClocks[0] = time;

		for (int i = 0; i < durations.Length; i++)
		{
			myClocks[i + 1] = (time += durations[i]);
		}
	}

	private float[] myClocks;

}
