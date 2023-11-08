using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldForKid.API;

public class ModelLogin
{
    public string username;
    public string password;
}

public class TestAPI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var person = new ModelLogin()
        {
            username = "111",
            password = "111"
        };
        //APIManager.Instance.Call<ModelLogin>("/user/login", person, HandleCallback);
    }

    private void HandleCallback(ModelLogin model)
    {
        Debug.Log(JsonConvert.SerializeObject(model));
    }
}
