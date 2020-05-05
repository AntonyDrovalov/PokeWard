using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Networking;


public class Register : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public GameObject confPassword;
    public GameObject text_error;
    private string Username;
    private string Password;
    private string ConfPassword;
    //private string form;
    private string data;
    private String[] Lines;

    public class JsonClass
    {
        public string Username;
        public string Password;
        public int Type;
    }

    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            data = uwr.downloadHandler.text;
            Lines = data.Split(',');
            Debug.Log("DATA: " + Lines.Length);
            bool UN = false;
            bool PW = false;
            bool CPW = false;

            
            if (Lines.Length == 1) //api check
            {
                UN = true;
            }
            else
            {
                text_error.GetComponent<Text>().text = "Username Taken";
                text_error.GetComponent<Text>().enabled = true;
                Debug.LogWarning("Username Taken");
            }

            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                text_error.GetComponent<Text>().text = "Password Must Be atleast 6 Characters long";
                text_error.GetComponent<Text>().enabled = true;
                Debug.LogWarning("Password Must Be atleast 6 Characters long");
            }

            if (ConfPassword == Password)
            {
                CPW = true;
            }
            else
            {
                text_error.GetComponent<Text>().text = "Passwords Dont match";
                text_error.GetComponent<Text>().enabled = true;
                Debug.LogWarning("Passwords Dont match");
            }

            if (UN == true && PW == true && CPW == true)
            {   
                bool Clear = true;
                int i = 1;
                foreach (char c in Password)
                {
                    if (Clear)
                    {
                        Password = "";
                        Clear = false;
                    }
                    i++;
                    char Encrypted = (char)(c * i);
                    Password += Encrypted.ToString();
                }
                
                
                //form = (Username + Environment.NewLine + Password);
                //System.IO.File.WriteAllText(@"C:/Anton/UnityTest/" + Username + ".txt", form); // api send req
                JsonClass form = new JsonClass();
                form.Username = Username;
                form.Password = Password;
                form.Type = 1;
                string json = JsonUtility.ToJson(form);

                StartCoroutine(PostRequest("https://quiet-crag-61602.herokuapp.com/users", json));
                username.GetComponent<InputField>().text = "";
                password.GetComponent<InputField>().text = "";
                confPassword.GetComponent<InputField>().text = "";
                text_error.GetComponent<Text>().text = "Registration Complete";
                text_error.GetComponent<Text>().enabled = true;
                print("Registration Complete");
                Application.LoadLevel("LoginScreen");
            }

        }
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
        StartCoroutine(GetRequest("https://quiet-crag-61602.herokuapp.com/users/" + Username));
    }
    
    void Start()
    {
        text_error.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confPassword.GetComponent<InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Username != "" && Password != "" && ConfPassword != "")
            {
                RegisterButton();
            }
        }

        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfPassword = confPassword.GetComponent<InputField>().text;
    }
}
