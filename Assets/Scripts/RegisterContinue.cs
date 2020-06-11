using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using static Globals;


public class RegisterContinue : MonoBehaviour
{

    public Toggle LadyToggle;
    public Toggle NinjaToggle;
    public Toggle AssistantToggle;

    public class JsonClass
    {
        public string username;
        public string password;
    }

    public class JsonType{
        public string username;
        public int typeId;
    }


    IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    public void toLoginButton()
    {
        Application.LoadLevel("LoginScreen");
    }

    public void RegisterButton()
    {
        JsonClass form = new JsonClass();
        form.username = Globals.Login_register;
        form.password = Globals.Password_register;
        string json = JsonUtility.ToJson(form);
        StartCoroutine(PostRequest("https://quiet-crag-61602.herokuapp.com/users", json));

        JsonType typeLady = new JsonType();
        typeLady.username = Globals.Login_register;
        typeLady.typeId = 1;
        string json_lady = JsonUtility.ToJson(typeLady);
        if(LadyToggle.isOn){
           StartCoroutine(PostRequest(" https://quiet-crag-61602.herokuapp.com/usertype", json_lady));
        }

        JsonType typeNinja = new JsonType();
        typeNinja.username = Globals.Login_register;
        typeNinja.typeId = 2;
        string json_ninja = JsonUtility.ToJson(typeNinja);
        if(NinjaToggle.isOn){
           StartCoroutine(PostRequest(" https://quiet-crag-61602.herokuapp.com/usertype", json_ninja));
        }

        JsonType typeAssistant = new JsonType();
        typeAssistant.username = Globals.Login_register;
        typeAssistant.typeId = 3;
        string json_assistant = JsonUtility.ToJson(typeAssistant);
        if(AssistantToggle.isOn){
           StartCoroutine(PostRequest(" https://quiet-crag-61602.herokuapp.com/usertype", json_assistant));
        }

        Application.LoadLevel("LoginScreen");  
    }

    public void BackButton(){
        Globals.Login_register = "";
        Globals.Password_register = "";
        Application.LoadLevel("RegisterScreen");
    }

    public void toTypeSelection(){
        Application.LoadLevel("TypeScene");
    }
    
    void Start()
    {
        Debug.Log("Loaded");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
