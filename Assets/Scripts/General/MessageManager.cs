using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    private Animator m_Animator;

    [SerializeField] private MessageData m_MessageData;
    [SerializeField] private GameObject m_MessageBox;
    [SerializeField] private TMPro.TextMeshProUGUI m_MessageText;

    private bool m_MessageBoxOpen;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_MessageBoxOpen)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseMessageBox();
            }
        }
    }

    private void OnEnable()
    {
        SocialBar.OnSocialBarDecrease += FirstSocialBarDecrease;
    }

    private void OnDisable()
    {
        SocialBar.OnSocialBarDecrease += FirstSocialBarDecrease;
    }

    // Set the message, turn the object to true and start animating
    private void OpenMessageBox(string message)
    {
        m_MessageText.text = message;

        m_MessageBox.SetActive(true);
        m_MessageBoxOpen = true;

        m_Animator.SetBool("MessageBoxOpen", m_MessageBoxOpen);
    }

    // Start the closing animation
    public void CloseMessageBox()
    {
        m_MessageBoxOpen = false;
        m_Animator.SetBool("MessageBoxOpen", m_MessageBoxOpen);
    }

    // Coroutine that gets called from the events
    private IEnumerator ShowMessageCo(string text)
    {
        OpenMessageBox(text);
        yield return new WaitForSeconds(4f);
        CloseMessageBox();
    }

    // When the messagebox is done closing, reset the text
    private void MessageBoxDoneClosing()
    {
        m_MessageBox.SetActive(false);
        m_MessageText.text = "";
    }

    private void FirstSocialBarDecrease()
    {
        StartCoroutine("ShowMessageCo", m_MessageData.FirstSocialBarDecrease);
    }

    //private void FirstNightOfSleep()
    //{
    //    StartCoroutine("ShowMessageCo", m_MessageData.FirstNightOfSleep);
    //}

}
