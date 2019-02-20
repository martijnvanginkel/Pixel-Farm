//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ApiCallPool : MonoBehaviour
//{
//    private static ApiCallPool m_Instance;
//    public static ApiCallPool Instance
//    {
//        get { return m_Instance; }
//        private set { m_Instance = value; }
//    }

//    [SerializeField]
//    private int m_PooledAmount = 5;
//    private bool m_WillGrow = true;

//    private List<ApiCall> m_PooledObjects;

//    void Awake()
//    {
//        if(m_Instance == null)
//        {
//            m_Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//        //DontDestroyOnLoad(gameObject);
//    }

//    void Start ()
//    {
//        CreatePool();
//	}

//    // Create X amount of gameobjects and add them to a list
//    private void CreatePool()
//    {
//        m_PooledObjects = new List<ApiCall>();

//        for (int i = 0; i < m_PooledAmount; i++)
//        {
//            ApiCall apiCall = NewPoolObject();
//            apiCall.gameObject.SetActive(false);
//        }
//    }
	 
//    public ApiCall GetPoolObject()
//    {
//        // Loop through the pool of objects and find the first object thats not already active
//        for (int i = 0; i < m_PooledObjects.Count; i++)
//        {
//            if(!m_PooledObjects[i].gameObject.activeInHierarchy)
//            {
//                m_PooledObjects[i].gameObject.SetActive(true);
//                return m_PooledObjects[i];
//            }
//        }

//        // If all objects are active, create a new object and add it to the list
//        if (m_WillGrow)
//        {
//            ApiCall apiCall = NewPoolObject();

//            return apiCall;
//        }

//        return null;
//    }

//    private ApiCall NewPoolObject()
//    {
//        GameObject obj = new GameObject();
//        ApiCall apiCall = obj.AddComponent<ApiCall>();
//        obj.transform.parent = transform;
//        m_PooledObjects.Add(apiCall);

//        return apiCall;
//    }

//    public void SetPoolObjectInactive(GameObject obj)
//    {
//        obj.SetActive(false);
//    }
//}
