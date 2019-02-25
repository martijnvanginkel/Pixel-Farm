using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeableObject : InteractableObject
{
    [SerializeField] protected ObjectData m_ObjectData; 

    //[SerializeField] private Sprite m_Icon;
    //public Sprite Icon
    //{
    //    get { return m_Icon; }
    //    set { m_Icon = value; }
    //}

    // Start is called before the first frame update
    //protected override void Start()
    //{
    //    base.Start();
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeItem()
    {
        base.PlayerActionEvent();
        Inventory.Instance.AddItem(m_ObjectData, 1);
        Destroy(this.gameObject);
    }
}
