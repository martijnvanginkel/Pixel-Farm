using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private SocialBar m_SocialBar;
    [SerializeField] private MoneyBar m_MoneyBar;

    private void OnEnable()
    {
        Npc.OnReceivedItem += DecideReceiveType;
    }

    private void OnDisable()
    {
        Npc.OnReceivedItem -= DecideReceiveType;
    }

    private void DecideReceiveType(ObjectData objectData, string receiveType)
    {
        switch (receiveType)
        {
            case "Social":
                m_SocialBar.GainPoints(objectData);
                break;
            case "Money":
                m_MoneyBar.GainMoney(objectData); 
                break;
            default:
                Debug.Log("No Data Received");
                break;
        }
    }
}
