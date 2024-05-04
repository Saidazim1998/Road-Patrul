using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PalkaState
{
	front,
	top,
	idle
}
public class Checker : MonoBehaviour
{
	public AudioSource winning;

	public PalkaState state;
	public static Checker instance;
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
		state = PalkaState.idle;
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("FrontChecker"))
		{
		   Debug.Log("Front Inside");
			state = PalkaState.front;
		}
		if (other.CompareTag("TopChecker"))
		{
			state = PalkaState.top;
			Debug.Log("Top Inside");
			foreach (Follower item in TrafficLightManager.instance.carListRight)
			{
				item.isStopForEvery = true;
			}
			foreach (Follower item in TrafficLightManager.instance.carListLeft)
			{
				item.isStopForEvery = true;
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("FrontChecker") || other.CompareTag("TopChecker"))
		{
			Debug.Log("Front Inside");
			state = PalkaState.idle;
		}
	}
}
