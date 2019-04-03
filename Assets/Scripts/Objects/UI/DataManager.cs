using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to decide what kind of item is received by an npc and triggers the right databar
public class DataManager : MonoBehaviour
{
    [SerializeField] private SocialBar m_SocialBar;
    [SerializeField] private MoneyBar m_MoneyBar;

    //private void OnEnable()
    //{
    //    InteractableObject.OnReceivedItem += DecideReceiveType;
    //}

    //private void OnDisable()
    //{
    //    InteractableObject.OnReceivedItem -= DecideReceiveType;
    //}

    private void DecideReceiveType(ObjectData objectData, string receiveType)
    {
        if(receiveType == null)
        {
            Debug.Log("null");
        }

        if (receiveType == "")
        {
            Debug.Log("null2");
        }

        switch (receiveType)
        {
            case "Social":
                m_SocialBar.IncreaseValue(objectData.SocialValue);
                break;
            case "Economy":
                //m_MoneyBar.IncreaseValue(objectData); 
                break;
            case "Environment":
                Debug.Log("Environment");
                break;
            default:
                Debug.Log("No Data Received");
                break;
        }
    }

}
