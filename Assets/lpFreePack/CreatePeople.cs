using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePeople : MonoBehaviour
{
    public Following[] people;
    public PathCreator[] paths;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(createP());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator createP()
    {
        while (count<10)
        {
            int randomPeaople = Random.Range(0, people.Length);
            int randompath = Random.Range(0, paths.Length);
            Following _people = Instantiate(people[randomPeaople]);
            _people.creator = paths[randompath];
            count++;
            yield return new WaitForSeconds(2f);
        }
    }
}
