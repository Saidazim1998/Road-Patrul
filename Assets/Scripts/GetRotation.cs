using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum rotation
{
	MP,
	IP,
	HY,
	INH
}
public class GetRotation : MonoBehaviour
{
	public rotation rotState;
	public bool MP;
	public bool IP;
	public bool HY;
	public bool INH;
	public static GetRotation instance;
	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		float degree = transform.eulerAngles.y;
		if (15>degree)
		{
			rotState = rotation.MP;
		}
		if (degree>345)
		{
			rotState = rotation.MP;

		}
		if (105 > degree && degree > 75)
		{
			rotState = rotation.IP;

		}
		if (195 > degree && degree > 165)
		{
			rotState = rotation.HY;

		}
		if (285 > degree && degree > 255)
		{
			rotState = rotation.INH;

		}
	}
}
