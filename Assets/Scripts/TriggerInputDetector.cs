using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class TriggerInputDetector : MonoBehaviour
{
    public GameObject menu;
    private InputData _inputData;
    private bool isActive = false;
    public GameObject player;
    [SerializeField]
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton))
        {
            if (Abutton)
            {
                menu.SetActive(true);
                //menu.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+offset);
                menu.transform.rotation = Quaternion.Euler(0f,player.transform.eulerAngles.y,0f);
            }
        }
    }
}
