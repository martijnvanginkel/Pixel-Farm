//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using UnityEngine;
//using UnityEngine.Networking;

//public class ApiCall : MonoBehaviour
//{
//    // Make a body of formfields for the Post Coroutine
//    private WWWForm MakeRequestBody(Dictionary<string, string> formFields)
//    {
//        WWWForm form = new WWWForm();

//        foreach (var item in formFields)
//        {
//            form.AddField(item.Key, item.Value);
//        }

//        return form;
//    }

//    // Call Get Coroutine
//    public void Get(string resource, Action<string> onComplete)
//    {
//        StartCoroutine(GetCo(resource, onComplete));
//    }

//    // Call Post Coroutine 
//    public void Post(Dictionary<string, string> body, string resource, Action<string> onComplete)
//    {
//        StartCoroutine(PostCo(body, resource, onComplete));
//    }

//    // Send Post Request
//    public IEnumerator PostCo(Dictionary<string, string> body, string resource, Action<string> onComplete)
//    {
//        WWWForm form = MakeRequestBody(body);
//        string uri = Environment.ServerLocation.API() + resource;

//        using (UnityWebRequest www = UnityWebRequest.Post(uri, form))
//        {
//            yield return www.SendWebRequest();
         
//            if (www.isNetworkError || www.isHttpError)
//            {
//                Debug.Log(www.error);
//            }
//            else
//            {
//                string result = www.downloadHandler.text;
//                onComplete(result);

//                Debug.Log("Form upload complete!");
//            }
//            // Set the poolobject back to inactive after the api call is done
//            ApiCallPool.Instance.SetPoolObjectInactive(this.gameObject);

//        }    
//    }

//    // Call Get Request
//    public IEnumerator GetCo(string resource, Action<string> onComplete)
//    {
//        string uri = Environment.ServerLocation.API() + resource;

//        using (UnityWebRequest www = UnityWebRequest.Get(uri))
//        {
//            UnityWebRequestAsyncOperation op = www.SendWebRequest();

//            while (!op.isDone)
//            {
//                yield return null;
//            }

//            string result = op.webRequest.downloadHandler.text;

//            if (www.isNetworkError || www.isHttpError)
//            {
//                Debug.LogError(www.error);
//            }

//            // Callback after getting back the result
//            onComplete(result);
//        }

//        // Set the poolobject back to inactive after the api call is done
//        ApiCallPool.Instance.SetPoolObjectInactive(this.gameObject);
//    }
//}