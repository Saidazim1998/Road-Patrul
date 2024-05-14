using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PalkaState
{
	front,
	top,
	right,
	idle
}
public enum RightRot
{
    MP,
    IP,
    HY,
    INH,
	none
}
public class Checker : MonoBehaviour
{
	public AudioSource winning;
	public GameObject winningUI;
	public GameObject player;
    public InputActionProperty triggerValue;
	public LayerMask layerMask;
    public PalkaState state;
	public RightRot rightRotState;
	public static Checker instance;
	bool isChecked = false;
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
        rightRotState = RightRot.none;

        state = PalkaState.idle;
	}

    public IEnumerator Winning()
    {
        if(!TrafficLightManager.instance.isGameOver) 
        {
            winningUI.SetActive(true);
            winningUI.transform.rotation = Quaternion.Euler(0f, player.transform.eulerAngles.y, 0f);

            winning.Play();
        }
        yield return new WaitForSeconds(3f);
        winningUI.SetActive(false);

    }
    // Update is called once per frame
    void Update()
	{
        float value = triggerValue.action.ReadValue<float>();
		if (value > 0.8f)
		{
			isChecked = true;
            RaycastHit hit;

            Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (Physics.Raycast(origin, transform.TransformDirection(Vector3.up), out hit, 20, layerMask))
            {
                Debug.DrawRay(origin, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.CompareTag("MP"))
                {
                    rightRotState = RightRot.MP;
                }
                else if (hit.collider.gameObject.CompareTag("INH"))
                {
                    rightRotState = RightRot.INH;

                }
                else if (hit.collider.gameObject.CompareTag("IP"))
                {
                    rightRotState = RightRot.IP;

                }
                else if (hit.collider.gameObject.CompareTag("HY"))
                {
                    rightRotState = RightRot.HY;

                }
                else
                {
                    rightRotState = RightRot.none;

                }
            }
        }
		else
		{
			if (isChecked && CheckerLeft.instance.leftRotState == RightRot.none)
			{
				isChecked = false;
				// start corutine
				StartCoroutine(StartWithDelay());
			}
			rightRotState = RightRot.none;
		}
       
    }
	IEnumerator StartWithDelay()
	{
		yield return new WaitForSeconds(5f);
		if (state != PalkaState.top)
		{
			foreach (Follower item in TrafficLightManager.instance.R1)
			{
				item.isStopForEvery = false;
			}
            foreach (Follower item in TrafficLightManager.instance.R2)
            {
                item.isStopForEvery = false;
            }
            foreach (Follower item in TrafficLightManager.instance.L1)
            {
                item.isStopForEvery = false;
            }
            foreach (Follower item in TrafficLightManager.instance.L2)
            {
                item.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.ambulance != null)
            {
                TrafficLightManager.instance.ambulance.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.pojar != null)
            {
                TrafficLightManager.instance.pojar.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.police != null)
            {
                TrafficLightManager.instance.police.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.merc != null)
            {
                TrafficLightManager.instance.merc.isStopForEvery = false;
            }
        }
	}
	private void OnTriggerStay(Collider other)
	{
        if (other.CompareTag("TopChecker"))
		{
			state = PalkaState.top;
            foreach (Follower item in TrafficLightManager.instance.R1)
            {
                item.isStopForEvery = true;
            }
            foreach (Follower item in TrafficLightManager.instance.R2)
            {
                item.isStopForEvery = true;
            }
            foreach (Follower item in TrafficLightManager.instance.L1)
            {
                item.isStopForEvery = true;
            }
            foreach (Follower item in TrafficLightManager.instance.L2)
            {
                item.isStopForEvery = true;
            }
            if (TrafficLightManager.instance.ambulance!=null)
            {
                TrafficLightManager.instance.ambulance.isStopForEvery = true;
            }
            if (TrafficLightManager.instance.pojar != null)
            {
                TrafficLightManager.instance.pojar.isStopForEvery = true;
            }
            if (TrafficLightManager.instance.police != null)
            {
                TrafficLightManager.instance.police.isStopForEvery = true;
            }
            if (TrafficLightManager.instance.merc != null)
            {
                TrafficLightManager.instance.merc.isStopForEvery = true;
            }
        }
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("TopChecker"))
		{
			state = PalkaState.idle;
			StartCoroutine(Delay());
        }
	}
	IEnumerator Delay()
	{
		yield return new WaitForSeconds(5f);
		if(Checker.instance.rightRotState == RightRot.none)
		{
            foreach (Follower item in TrafficLightManager.instance.R1)
            {
                item.isStopForEvery = false;
            }
            foreach (Follower item in TrafficLightManager.instance.R2)
            {
                item.isStopForEvery = false;
            }
            foreach (Follower item in TrafficLightManager.instance.L1)
            {
                item.isStopForEvery = false;
            }
            foreach (Follower item in TrafficLightManager.instance.L2)
            {
                item.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.ambulance != null)
            {
                TrafficLightManager.instance.ambulance.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.pojar != null)
            {
                TrafficLightManager.instance.pojar.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.police != null)
            {
                TrafficLightManager.instance.police.isStopForEvery = false;
            }
            if (TrafficLightManager.instance.merc != null)
            {
                TrafficLightManager.instance.merc.isStopForEvery = false;
            }
        }
	}
}
