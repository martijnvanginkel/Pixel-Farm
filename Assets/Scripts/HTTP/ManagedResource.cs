//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class ManagedResource
//{
//    protected string m_ResourceType;
//    public string ResourceType
//    {
//        set { m_ResourceType = value; }
//    }

//    protected int m_ID = -1;
//    public int ID
//    {
//        get { return m_ID; }
//        set { m_ID = value; }
//    }

//    public ManagedResource(string resourceType)
//    {
//        m_ResourceType = resourceType;
//    }

//    private string m_JSONData = "";

//    public abstract Dictionary<string, string> PopulateBody();

//    public void Add<T>(MonoBehaviour monoBehaviour, Action<T> callback) where T : ManagedJson, new()
//    {
//        monoBehaviour.StartCoroutine(AddCo<T>(PopulateBody(), callback));
//    }

//    private IEnumerator AddCo<T>(Dictionary<string, string> body, Action<T> callback) where T : ManagedJson, new()
//    {
//        m_JSONData = "";

//        yield return PostCall(PopulateBody(), OnLoadComplete);
//        yield return new WaitForDataLoad(() => (m_JSONData == ""));

//        callback(OnAdd<T>(m_JSONData));
//        m_JSONData = "";

//        yield return null;
//    }

//    // Implement what needs to happen if the item is added
//    //protected abstract void OnAdd(string result);
//    protected abstract T OnAdd<T>(string json) where T : ManagedJson, new();

//    public void LoadAll<T>(MonoBehaviour monoBehaviour, Action<List<T>> callback) where T : ManagedJson
//    {
//        monoBehaviour.StartCoroutine(LoadAllCo<T>(callback));
//    }

//    // Call LoadAll to receive all items from the database
//    private IEnumerator LoadAllCo<T>(Action<List<T>> callback) where T : ManagedJson
//    {
//        m_JSONData = "";

//        // Go to OnLoadComplete once GetCall is done and return json from there
//        yield return GetCall(m_ResourceType, OnLoadComplete);
//        yield return new WaitForDataLoad(() => (m_JSONData == ""));

//        // Callback to RandomScript with the jsondata
//        callback(OnLoadAll<T>(m_JSONData));
//        m_JSONData = "";

//        yield return null;
//    }

//    protected abstract List<T> OnLoadAll<T>(string json) where T : ManagedJson;

//    public void Load<T>(int id, MonoBehaviour monoBehaviour, Action<T> callback) where T : ManagedJson, new()
//    {
//        monoBehaviour.StartCoroutine(LoadCo<T>(id, callback));
//    }

//    private IEnumerator LoadCo<T>(int id, Action<T> callback) where T : ManagedJson, new()
//    {
//        m_JSONData = "";

//        yield return GetCall(CallWithID(id), OnLoadComplete);
//        yield return new WaitForDataLoad(() => (m_JSONData == ""));

//        callback(OnLoad<T>(m_JSONData));
//        m_JSONData = "";

//        yield return null;
//    }

//    protected abstract T OnLoad<T>(string json) where T : ManagedJson, new();
    
//    private void OnLoadComplete(string JSONData)
//    {
//        m_JSONData = JSONData;
//    }

//    private string CallWithID(int id)
//    {
//        return m_ResourceType + "/" + id.ToString();
//    }

//    private IEnumerator PostCall(Dictionary<string, string> body, Action<string> onComplete)
//    {
//        yield return ApiCallPool.Instance.GetPoolObject().PostCo(body, m_ResourceType, onComplete); // actually PostCo
//    }

//    private IEnumerator GetCall(string resource, Action<string> onComplete)
//    {
//        yield return ApiCallPool.Instance.GetPoolObject().GetCo(resource, onComplete);
//    }
//}

//class WaitForDataLoad : CustomYieldInstruction
//{
//    Func<bool> m_Predicate;

//    //Constructor
//    public WaitForDataLoad(Func<bool> predicate) { m_Predicate = predicate; }

//    public override bool keepWaiting { get { return m_Predicate(); } }
//}

