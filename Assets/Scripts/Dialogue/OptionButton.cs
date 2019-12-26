using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button m_Button;
    private DialogueOption m_CurrentOption;

    [SerializeField] private TMPro.TextMeshProUGUI m_Text;

    private void SetButtonText(string text)
    {
        m_Text.text = text;
    }

    public void SetButtonVariables(DialogueOption option)
    {
        m_CurrentOption = option;
        SetButtonText(m_CurrentOption.OptionText);

        m_Button.onClick.AddListener(delegate () { DialogueManager.Instance.NextNode(m_CurrentOption.NextNodeID); });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Text.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Text.color = Color.white;
    }
}
