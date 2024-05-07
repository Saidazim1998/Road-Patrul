using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public string carName;
    public GameObject[] cars;
    public GameObject ambulance;
    public PathCreator creator;
    public bool isEnter = false;
    public float speed = 2f;
    float distanceTravelled;
    [SerializeField]
    public float limitDistance = 0.027f;
    [SerializeField]
    public float maxDistance = 20f;
    public LayerMask layerMask;
    public bool isStop = false;
    public bool isRed = false;
    public bool isStopForEvery = false;
    public bool isCrossed = false;
    public int index;
    // Start is called before the first frame update
    private void OnEnable()
    {
        index = Random.Range(0, cars.Length);
        //foreach (GameObject car in cars)
        //{
        //    car.SetActive(false);
        //}
        cars[index].SetActive(true);
    }
    

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        Vector3 origin = new Vector3(transform.position.x , transform.position.y + 0.5f, transform.position.z );
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out hit,maxDistance,layerMask))
        {
            if (Vector3.Distance(origin, hit.point) < 4)
			{
				isStop = true;
			}
			else
			{
				isStop = false;
			}
        }
        if ((isRed || isStopForEvery) && !isCrossed)
		{
			if (isEnter)
			{
                speed = 0;
            }
			else
			{
                if (isStop)
                {
                    speed = 0;
                }
				else
				{
                    speed = 2;
				}
            }
		}
		else
		{
            speed = 2;
        }
        distanceTravelled+=speed*Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = creator.path.GetRotationAtDistance(distanceTravelled);
    }
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("TriggerR"))
		{
            isEnter = false;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("TriggerR"))
        {
            isEnter = true;
        }
        if (other.CompareTag("chorraxa"))
        {
            isCrossed = true;
            if(name == "R1")
            {
                --TrafficLightManager.instance.countCarHY;
                TrafficLightManager.instance.R1.Remove(this);
            }
            if (name == "R2")
            {
                --TrafficLightManager.instance.countCarMP;
                TrafficLightManager.instance.R2.Remove(this);
            }
            if (name == "L1")
            {
                --TrafficLightManager.instance.countCarIP;
                TrafficLightManager.instance.L1.Remove(this);
            }
            if (name == "L2")
            {
                --TrafficLightManager.instance.countCarINH;
                TrafficLightManager.instance.L2.Remove(this);
            }
        }
        if (other.CompareTag("destroy"))
        {
            Destroy(gameObject);
        }

    }
	

}
