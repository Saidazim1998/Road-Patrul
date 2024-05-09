using PathCreation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Following : MonoBehaviour
{
    public PathCreator creator;
    float distanceTravelled;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = creator.path.GetRotationAtDistance(distanceTravelled);
    }
}
