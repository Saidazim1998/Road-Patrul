using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.InputSystem;
public class TrafficLightManager : MonoBehaviour
{
    public Material redLightsForRight;
    public Material redLightsForLeft;
    public Material greenLightsForLeft;
    public Material greenLightsForRight;
    public Material yellowLights;
    [SerializeField]
    public bool isRedForRight;
    [SerializeField]
    public bool isRedForLeft;
    public Follower carPrefab;
    public FollowerForSpecial ambulancePrefab;
    public FollowerForSpecial pojarPrefab;
    public FollowerForSpecial mercPrefab;
    FollowerForSpecial ambulance;
    FollowerForSpecial pojar;
    FollowerForSpecial merc;
    public List<Follower> carListRight;
    public List<Follower> carListLeft;
    public List<Follower> R1_1;
    public List<Follower> R1_2;
    public List<Follower> R2_1;
    public List<Follower> R2_2;
    public List<Follower> L1_1;
    public List<Follower> L1_2;
    public List<Follower> L2_1;
    public List<Follower> L2_2;
    public PathCreator[] rightLightPaths;
    public PathCreator[] leftLightPaths;
    public static TrafficLightManager instance;
    private bool isCanCheck = false;
    public bool isCanCreateAmbulance = false;
    private bool isCanCreatepojarniy = false;
    private bool isCanCreateMerc = false;
    public InputActionProperty triggerValue;
	private void Awake()
	{
        if (instance == null)
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
        StartCoroutine(TrafficLight());
        StartCoroutine(CreateCarForRight());
        StartCoroutine(CreateCarForLeft());
    }

    // Update is called once per frame
    void Update()
    {
		if (isCanCreateAmbulance && Checker.instance.state == PalkaState.top)
		{
            ambulance = Instantiate(ambulancePrefab);
            isCanCreateAmbulance=false;
		}
        if (isCanCreatepojarniy && Checker.instance.state == PalkaState.top)
        {
            pojar = Instantiate(pojarPrefab);
            isCanCreatepojarniy = false;
        }
        if (isCanCreateMerc && Checker.instance.state == PalkaState.top)
        {
            merc = Instantiate(mercPrefab);
            isCanCreateMerc = false;
        }
        float value = triggerValue.action.ReadValue<float>();
		foreach (Follower item in carListRight)
		{
            item.isRed = isRedForRight;
		}
        foreach (Follower item in carListLeft)
		{
            item.isRed = isRedForLeft;
		}

		if (value>=0.8f)
		{
            // R1 yonalishni ochish
            if (GetRotation.instance.rotState == rotation.INH && Checker.instance.state == PalkaState.front)
            {
                ambulance.isRed = false;
                foreach (Follower item in R1_1)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                foreach (Follower item in R1_2)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
            }
            // R2 yonalishni ochish
            if (GetRotation.instance.rotState == rotation.IP && Checker.instance.state == PalkaState.front)
            {
                foreach (Follower item in R2_1)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                foreach (Follower item in R2_2)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
            }
            // L1 yonalishni ochish
            if (GetRotation.instance.rotState == rotation.HY && Checker.instance.state == PalkaState.front)
            {
                pojar.isRed = false;
                foreach (Follower item in L1_1)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                foreach (Follower item in L1_2)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
            }
            // L2 yonalishni ochish
            if (GetRotation.instance.rotState == rotation.MP && Checker.instance.state == PalkaState.front)
            {
                foreach (Follower item in L2_1)
                {
                    merc.isRed = false;
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                foreach (Follower item in L2_2)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
            }
        }
    }
    public void makeTrueForCheck()
	{
        isCanCheck = true;
	}
    public void makeFalseForCheck()
    {
        isCanCheck = false;
    }
    public void MakeCreateAmbulance()
	{
        isCanCreateAmbulance = true;
	}
    public void MakeCreatePojar()
    {
        isCanCreatepojarniy = true;
    }
    public void MakeCreateMerc()
    {
        isCanCreateMerc = true;
    }

    public IEnumerator TrafficLight()
	{
		while (true)
		{
            greenLightsForRight.DisableKeyword("_EMISSION");
            redLightsForLeft.DisableKeyword("_EMISSION");

            redLightsForRight.EnableKeyword("_EMISSION");
            greenLightsForLeft.EnableKeyword("_EMISSION");
            isRedForRight = true;
            yellowLights.EnableKeyword("_EMISSION");

            yield return new WaitForSeconds(2f);
            yellowLights.DisableKeyword("_EMISSION");

            isRedForLeft = false;

            yield return new WaitForSeconds(8f);

            greenLightsForLeft.DisableKeyword("_EMISSION");
            redLightsForRight.DisableKeyword("_EMISSION");
            redLightsForLeft.EnableKeyword("_EMISSION");
            greenLightsForRight.EnableKeyword("_EMISSION");
            isRedForLeft = true;
            yellowLights.EnableKeyword("_EMISSION");

            yield return new WaitForSeconds(2f);
            yellowLights.DisableKeyword("_EMISSION");

            isRedForRight = false;

            yield return new WaitForSeconds(8f);

        }
        
	}
    public IEnumerator CreateCarForRight()
	{
        int counter = 0;
		while (counter<12)
		{
            for (int i = 0; i < rightLightPaths.Length; i++)
            {
                int indexR = Random.Range(0, 3);
                int indexL = Random.Range(3, 6);

                Follower car = Instantiate(carPrefab);
                car.creator = rightLightPaths[indexR];
				if (indexR == 2 || indexR == 1)
				{
                    R1_2.Add(car);
				}
                if(indexR == 0)
				{
                    R1_1.Add(car);
				}
                carListRight.Add(car);
                Follower car2 = Instantiate(carPrefab);
                car2.creator = rightLightPaths[indexL];
                if (indexL == 4 || indexL == 5)
                {
                    R2_2.Add(car2);
                }
                if (indexL == 3)
                {
                    R2_1.Add(car2);
                }
                carListRight.Add(car2);
                
                yield return new WaitForSeconds(1.5f);
                counter++;
            }
        }
	}
    public IEnumerator CreateCarForLeft()
    {
        int counter = 0;
        while (counter < 12)
        {
            for (int i = 0; i < rightLightPaths.Length; i++)
            {
                int indexR = Random.Range(0, 3);
                int indexL = Random.Range(3, 6);

                Follower car = Instantiate(carPrefab);
                car.creator = leftLightPaths[indexR];
                if (indexR == 2 || indexR == 1)
                {
                    L1_2.Add(car);
                }
                if (indexR == 0)
                {
                    L1_1.Add(car);
                }
                carListLeft.Add(car);
                Follower car2 = Instantiate(carPrefab);
                car2.creator = leftLightPaths[indexL];
                if (indexR == 2 || indexR == 1)
                {
                    L2_2.Add(car2);
                }
                if (indexR == 0)
                {
                    L2_1.Add(car2);
                }
                carListLeft.Add(car2);

                yield return new WaitForSeconds(1.5f);
                counter++;
            }
        }
    }
    
}
