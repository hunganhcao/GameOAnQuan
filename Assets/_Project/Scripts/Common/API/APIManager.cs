using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using XXX.SO.Event;
using XXX.UI.Popup;

namespace WorldForKid.API
{
    public class APIManager : SingletonDontDestroy<APIManager>
    {
        public void Call<T>(string endpoint, Constain.Method method, object data, Action<T> handel)
        {
            switch (method)
            {
                case Constain.Method.GET:
                    StartCoroutine(RequestGet(endpoint, handel));
                    break;
                case Constain.Method.POST:
                    StartCoroutine(RequestPost(endpoint, data, handel));
                    break;
            }
        }
        public static IEnumerator RequestPost<T>(string endpoint, object data, Action<T> handel)
        {
            PopupManager.Instance.ShowPopupLoading(null); 
            string url = string.Format(Constain.URLAPI, DataManager.IpServer, DataManager.PortAPI) + endpoint;

            string json = JsonConvert.SerializeObject(data);

            WWWForm form = new WWWForm();
            form.AddField("data", json);

            Debug.Log(string.Format("API send: {0} || {1}", url, json));

            UnityWebRequest www = UnityWebRequest.Post(url, form);
            //www.SetRequestHeader("Content-Type", "application/json");


            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                handel(default);
            }
            else
            {
                var jsonRespone = www.downloadHandler.text;
                var respone = JsonConvert.DeserializeObject<BaseRespone<T>>(jsonRespone);

                Debug.Log(string.Format("API receive: {0} || {1}", endpoint, jsonRespone));
                if (respone != null)
                {
                    if (respone.code == 200)
                    {
                        handel?.Invoke(respone.data);
                    }
                    else
                    {
                        PopupManager.Instance.ShowPopupWarming(respone.message);
                    }
                }
            }
            PopupManager.Instance.HidePopupLoading();
        }
        public static IEnumerator RequestGet<T>(string endpoint, Action<T> handel)
        {
            PopupManager.Instance.ShowPopupLoading(null);
            string url = string.Format(Constain.URLAPI, DataManager.IpServer, DataManager.PortAPI) + endpoint;

            Debug.Log(string.Format("API send GET: {0}", url));

            UnityWebRequest www = UnityWebRequest.Get(url);
            www.SetRequestHeader("Content-Type", "application/json");
            if(!string.IsNullOrEmpty(DataManager.currentPlayer.Id))
                www.SetRequestHeader("ID", DataManager.currentPlayer.Id);


            yield return www.SendWebRequest();


            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                handel(default);
            }
            else
            {
                var jsonRespone = www.downloadHandler.text;
                var respone = JsonConvert.DeserializeObject<BaseRespone<T>>(jsonRespone);
                
                Debug.Log(string.Format("API receive: {0} || {1}", endpoint, jsonRespone));
                
                if (respone != null)
                {
                    if (respone.code == 200)
                    {
                        handel?.Invoke(respone.data);
                    }
                    else
                    {
                        PopupManager.Instance.ShowPopupWarming(respone.message);
                    }
                }
            }
            PopupManager.Instance.HidePopupLoading();
        }
    }
}
