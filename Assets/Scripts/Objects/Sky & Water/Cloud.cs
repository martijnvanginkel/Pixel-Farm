using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloud : MonoBehaviour
{
    private Animator m_Animator;
    private RectTransform m_RectTransform;
    private Image m_Image;

    [SerializeField] private List<Sprite> m_CloudSprites = new List<Sprite>();

    [SerializeField] private float m_FirstCloudStartingPoint;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_RectTransform = GetComponent<RectTransform>();
        m_Image = GetComponent<Image>();
        m_Animator.Play("cloud_moving", 0, m_FirstCloudStartingPoint);
    }

    private void SpawnNewCloud()
    {
        m_Image.sprite = RandomSprite();
        m_Image.SetNativeSize();
        m_RectTransform.anchoredPosition = new Vector3(m_RectTransform.anchoredPosition.x, RandomHeight());
        m_Animator.SetFloat("Speed", RandomSpeed());
    }

    private float RandomHeight()
    {
        float randomHeight = Random.Range(90f, 190f);

        return Mathf.Round(randomHeight);
    }

    private Sprite RandomSprite()
    {
        int randomInt = Random.Range(0, m_CloudSprites.Count);

        return m_CloudSprites[randomInt];
    }

    private float RandomSpeed()
    {
        float speed = Random.Range(0.1f, 0.5f);

        return speed;
    }

}
