using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFish : MonoBehaviour
{
    public delegate void FishCaught();
    public static event FishCaught OnFishCaught;

    private Rigidbody2D m_RB;
    private SpriteRenderer m_SpriteRenderer;
    private float m_UpForce;
    private float m_LeftForce;
    private float m_RotationForce;
    private bool m_InAir;
    private ObjectData m_MyFish;

    [SerializeField] private List<ObjectData> m_FishDataList = new List<ObjectData>();

    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_RB = GetComponent<Rigidbody2D>();
        m_MyFish = FindFish();
        m_SpriteRenderer.sprite = m_MyFish.PrefabSprite;
        m_UpForce = Random.Range(120f, 180f);
        m_LeftForce = Random.Range(70f, 110f);
        m_RotationForce = Random.Range(70f, 220f);

        
        m_RB.AddForce(transform.up * m_UpForce);
        m_RB.AddForce(Vector3.left * m_LeftForce);
        m_InAir = true;
    }

    private ObjectData FindFish()
    {
        int listLength = m_FishDataList.Count;
        int randomSize = Random.Range(0, listLength);
       
        return (m_FishDataList[randomSize]);
    }

    void Update()
    {
        transform.Rotate(0, 0, m_RotationForce * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && m_InAir)
        {
            Inventory.Instance.AddItem(m_MyFish, 1);
            OnFishCaught?.Invoke();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("GroundTile"))
        {
            Instantiate(m_MyFish.Prefab, new Vector3(this.transform.position.x, -2.7f, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }


}
