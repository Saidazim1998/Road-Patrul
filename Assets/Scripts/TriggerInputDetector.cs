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
               
            }
        }
    }
}
