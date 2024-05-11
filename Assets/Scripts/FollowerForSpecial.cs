using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerForSpecial : MonoBehaviour
{
    public GameObject myCar;
    public PathCreator creator;
    public string name = "";
    public bool isEnter = false;
    public float speed = 2f;
    float distanceTravelled;
    [SerializeField]
    public float limitDistance = 0.027f;
    [SerializeField]
    public float maxDistance = 20f;
    public LayerMask layerMask;
    public bool isStop = false;
    public bool isRed = true;
    public bool isStopForEvery = false;
    public bool isFinish = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTime());
        isRed = true;
		if (name == "a")
		{
            creator = TrafficLightManager.instance.rightLightPaths[0];
        }
        if (name == "p")
		{
            creator = TrafficLightManager.instance.leftLightPaths[1];
        }
        if (name == "m")
        {
            creator = TrafficLightManager.instance.leftLightPaths[4];
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (Vector3.Distance(origin, hit.point) < 4)
            {
                isStop = true;
            }
            else
            {
                isStop = false;
            }
        }
       if(!TrafficLightManager.instance.isGameOver)
        {
            if (isRed || isStopForEvery)
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
        }
        else
        {
            speed = 0;
        }
        distanceTravelled += speed * Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = creator.path.GetRotationAtDistance(distanceTravelled);
    }
    IEnumerator StartTime()
    {
        yield return new WaitForSeconds(15f);
        if (!isFinish)
        {
            Debug.Log("GameOver");
            TrafficLightManager.instance.OnGameOver();
        }
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
            isFinish = true;
        }
        if (other.CompareTag("destroy"))
        {
            Checker.instance.Winning();
            Destroy(gameObject);
        }
        if (other.CompareTag("Car"))
        {
            Debug.Log("collide");

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("collide");
        }
    }
}
