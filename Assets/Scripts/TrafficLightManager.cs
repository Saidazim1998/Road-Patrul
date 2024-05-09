using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.InputSystem;
public class TrafficLightManager : MonoBehaviour
{
    public int countCarMP = 0;
    public int countCarINH = 0;
    public int countCarHY = 0;
    public int countCarIP = 0;
    public GameObject gameOverUI;
    public AudioSource audioSource;
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
    [HideInInspector]
    public FollowerForSpecial policePrefab;
    [HideInInspector]
    public FollowerForSpecial ambulance;
    [HideInInspector]
    public FollowerForSpecial pojar;
    [HideInInspector]
    public FollowerForSpecial merc;
    public FollowerForSpecial police;
    public List<Follower> R1;
    public List<Follower> R2;
    public List<Follower> L1;
    public List<Follower> L2;
    public PathCreator[] rightLightPaths;
    public PathCreator[] leftLightPaths;
    public static TrafficLightManager instance;
    private bool isCanCheck = false;
    public bool isCanCreateAmbulance = false;
    private bool isCanCreatepojarniy = false;
    private bool isCanCreateMerc = false;
    public bool isGameOver= false;
    bool go = false;
    private bool isCanCreateCarR= true;
    private bool isCanCreateCarL= true;
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
    public void OnGameOver()
    {
        if (go)
        {
            gameOverUI.SetActive(true);
            audioSource.Play();
            go = false;
        }
            isGameOver = true;
            foreach (Follower item in R1)
            {
                item.isStopForEvery = true;
            }
            foreach (Follower item in R2)
            {
                item.isStopForEvery = true;
            }
            foreach (Follower item in L1)
            {
                item.isStopForEvery = true;
            }
            foreach (Follower item in L2)
            {
                item.isStopForEvery = true;
            }
            if (ambulance != null)
            {
                ambulance.isStopForEvery = true;
            }
            if (pojar != null)
            {
                pojar.isStopForEvery = true;
            }
            if (police != null)
            {
                police.isStopForEvery = true;
            }
            if (merc != null)
            {
                merc.isStopForEvery = true;
            }

    }
	// Start is called before the first frame update
	void Start()
    {
        StartCoroutine(TrafficLight());
        StartCoroutine(CreateCar());
    }

    IEnumerator CreateAmbulance()
    {
        yield return new WaitForSeconds(15f);
        ambulance = Instantiate(ambulancePrefab);

    }
    IEnumerator CreatePojar()
    {
        yield return new WaitForSeconds(15f);
        pojar = Instantiate(pojarPrefab);

    }
    IEnumerator CreateMers()
    {
        yield return new WaitForSeconds(15f);
        merc = Instantiate(mercPrefab);
        yield return new WaitForSeconds(1f);
        police = Instantiate(policePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
        // ambulance create
        if (isCanCreateAmbulance && Checker.instance.state == PalkaState.top)
		{
            StartCoroutine(CreateAmbulance());
            isCanCreateAmbulance=false;
		}
        // pojar create
        if (isCanCreatepojarniy && Checker.instance.state == PalkaState.top)
        {
            StartCoroutine(CreatePojar());
            isCanCreatepojarniy = false;
        }
        //merc cerate
        if (isCanCreateMerc && Checker.instance.state == PalkaState.top)
        {
            StartCoroutine(CreateMers());
            isCanCreateMerc = false;
        }
        float value = triggerValue.action.ReadValue<float>();
		foreach (Follower item in R1)
		{
            item.isRed = isRedForRight;
		}
        foreach (Follower item in R2)
        {
            item.isRed = isRedForRight;
        }
        foreach (Follower item in L1)
		{
            item.isRed = isRedForLeft;
		}
        foreach (Follower item in L2)
        {
            item.isRed = isRedForLeft;
        }

        if (value>=0.8f)
		{
            // L1 va L2 yonalishni ochish
            if (
                (Checker.instance.rightRotState == RightRot.INH && CheckerLeft.instance.leftRotState == RightRot.IP)
                ||
                (Checker.instance.rightRotState == RightRot.IP && CheckerLeft.instance.leftRotState == RightRot.INH)
                )
            {
                if(merc!=null)
                {
                    merc.isRed = false;
                    police.isRed = false;
                }
                if (pojar != null)
                {
                    pojar.isRed = false;
                }
                foreach (Follower item in L1)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                foreach (Follower item in L2)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }

            }
            // R1 va R2 yonalishni ochish
            if (
               (Checker.instance.rightRotState == RightRot.HY && CheckerLeft.instance.leftRotState == RightRot.MP)
               ||
               (Checker.instance.rightRotState == RightRot.MP && CheckerLeft.instance.leftRotState == RightRot.HY)
               )
            {
                if (ambulance != null)
                {
                    ambulance.isRed = false;
                }
                foreach (Follower item in R1)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                foreach (Follower item in R2)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }

            }



            // R1 yonalishni ochish
            if (Checker.instance.rightRotState == RightRot.INH && CheckerLeft.instance.leftRotState == RightRot.none)
            {
                if(ambulance !=null)
                {
                    ambulance.isRed = false;
                }
                foreach (Follower item in R1)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                
            }
            // R2 yonalishni ochish
            if (Checker.instance.rightRotState == RightRot.IP && CheckerLeft.instance.leftRotState == RightRot.none)
            {
                foreach (Follower item in R2)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
               
            }
            // L1 yonalishni ochish
            if (Checker.instance.rightRotState == RightRot.HY && CheckerLeft.instance.leftRotState == RightRot.none)
            {
                if (pojar != null)
                {
                   pojar.isRed = false;
                }
                foreach (Follower item in L1)
                {
                    item.isStopForEvery = false;
                    item.isRed = false;
                }
                
            }
            // L2 yonalishni ochish
            if (Checker.instance.rightRotState == RightRot.MP && CheckerLeft.instance.leftRotState == RightRot.none)
            {
                if (merc != null)
                {
                    merc.isRed = false;

                }
                if (police != null)
                {
                    police.isRed = false;

                }
                foreach (Follower item in L2)
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
    
    public IEnumerator CreateCar()
	{
        while (true)
        {
            if (countCarMP < 15)
            {
                int indexMP = Random.Range(3, 6);
                Follower car = Instantiate(carPrefab);
                car.creator = rightLightPaths[indexMP];
                car.name = "R2";
                if (R2.Count>0)
                {
                    car.isStopForEvery = R2[0].isStopForEvery;
                }
                R2.Add(car);
                countCarMP++;
            }
            if (countCarHY < 15)
            {
                int indexHY = Random.Range(0, 3);
                Follower car = Instantiate(carPrefab);
                car.creator = rightLightPaths[indexHY];
                car.name = "R1";
                if (R1.Count > 0)
                {
                    car.isStopForEvery = R1[0].isStopForEvery;
                }
                R1.Add(car);
                countCarHY++;
            }
            if (countCarINH < 15)
            {
                int indexINH = Random.Range(3, 6);
                Follower car = Instantiate(carPrefab);
                car.creator = leftLightPaths[indexINH];
                car.name = "L2";
                if (L2.Count > 0)
                {
                    car.isStopForEvery = L2[0].isStopForEvery;
                }
                L2.Add(car);
                countCarINH++;
            }
            if (countCarIP < 15)
            {
                int indexIP = Random.Range(0, 3);
                Follower car = Instantiate(carPrefab);
                car.creator = leftLightPaths[indexIP];
                car.name = "L1";
                if (L1.Count > 0)
                {
                    car.isStopForEvery = L1[0].isStopForEvery;
                }
                L1.Add(car);
                countCarIP++;
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

}
