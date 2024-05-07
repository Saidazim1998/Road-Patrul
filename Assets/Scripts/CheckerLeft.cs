using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckerLeft : MonoBehaviour
{
    public LayerMask layerMask;
    public InputActionProperty triggerValue;

    public RightRot leftRotState;

    public static CheckerLeft instance;
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
        leftRotState = RightRot.none;
    }

    // Update is called once per frame
    void Update()
    {
        float value = triggerValue.action.ReadValue<float>();
        if (value > 0.8f)
        {
            RaycastHit hit;

            Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (Physics.Raycast(origin, transform.TransformDirection(Vector3.forward), out hit, 20, layerMask))
            {
                Debug.DrawRay(origin, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.CompareTag("MP"))
                {
                    leftRotState = RightRot.MP;
                    Debug.Log("MP");
                }
                else if (hit.collider.gameObject.CompareTag("INH"))
                {
                    leftRotState = RightRot.INH;


                    Debug.Log("INH");
                }
                else if (hit.collider.gameObject.CompareTag("IP"))
                {
                    leftRotState = RightRot.IP;


                    Debug.Log("IP");
                }
                else if (hit.collider.gameObject.CompareTag("HY"))
                {
                    leftRotState = RightRot.HY;


                    Debug.Log("HY");
                }
                else
                {
                    leftRotState = RightRot.none;

                }
            }
        }
        else
        {
            leftRotState = RightRot.none;
        }
    }
        
}
