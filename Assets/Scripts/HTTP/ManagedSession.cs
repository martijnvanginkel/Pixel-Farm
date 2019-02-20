//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ManagedSession : ManagedJson
//{

//    private string m_Name;
//    public string Name
//    {
//        get { return m_Name; }
//        set { m_Name = value; }
//    }

//    public ManagedSession() : base("session") { }

//    public override Dictionary<string, string> PopulateBody()
//    {
//        Dictionary<string, string> body = new Dictionary<string, string>
//        {
//            { "name", m_Name }
//        };

//        return body;
//    }
//}
