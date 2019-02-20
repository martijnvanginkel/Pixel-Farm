//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//public abstract class ManagedJson : ManagedResource
//{
//    public override abstract Dictionary<string, string> PopulateBody();

//    public ManagedJson(string resource) : base(resource) { }

//    protected override T OnAdd<T>(string json)
//    {
//        T result = new T();
//        result = JsonConvert.DeserializeObject<T>(json);

//        return result;
//    }

//    protected override List<T> OnLoadAll<T>(string json)
//    {
//        List<T> result = JArray.Parse(json).ToObject<List<T>>();

//        return result;
//    }

//    protected override T OnLoad<T>(string json)
//    {
//        T result = new T();
//        result = JsonConvert.DeserializeObject<T>(json);

//        return result;
//    }
//}
