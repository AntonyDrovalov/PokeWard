using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using static Globals;

public class Login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    public GameObject text_error;
    private string Username;
    private string Password;
    //private String[] Lines;
    private string DecryptedPass;
    private string data;
    private String[] Lines_data;
    private List<string> Lines = new List<string>();


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
            DecryptedPass = "";
            data = uwr.downloadHandler.text;
            Lines_data = data.Split(',');
            //Debug.Log("DATA: " + Lines_data.Length);
            //Debug.Log("Username: " + Lines_data[0]);
            //Debug.Log("Password: " + Lines_data[1]);
            
            bool UN = false;
            bool PW = false;
            
            if (Lines_data.Length == 2) //api check
            {
                UN = true;
                //List<string> Lines = new List<string>();
                Lines.Clear();
                Lines.Add(Lines_data[0].Split(':')[1]); // username
                Lines.Add(Lines_data[1].Split(':')[1]); //encrypted password 
            }
            else
            {
                text_error.GetComponent<Text>().text = "Username Invalid";
                text_error.GetComponent<Text>().enabled = true;
                Debug.LogWarning("Username Invalid");
            }
            if (Lines_data.Length == 2)
            {
                Lines[1] = Lines[1].Trim('"','}');
                //DecryptedPass = Lines[1];
                //DecryptedPass = DecryptedPass.Trim('"');

                int i = 1;
                foreach (char c in Lines[1])
                {
                    i++;
                    char Decrypted = (char)(c / i);
                    DecryptedPass += Decrypted.ToString();
                }
                
                if (Password == DecryptedPass)
                {
                    PW = true;
                }
                else
                {
                    text_error.GetComponent<Text>().text = "Password is Invalid";
                    Debug.LogWarning(Password);
                    Debug.LogWarning(DecryptedPass);
                    Debug.LogWarning("Password is Invalid");
                    text_error.GetComponent<Text>().enabled = true;
                }
            }
            
            if (UN == true && PW == true)
            {
                Globals.Global_user = Username;
                username.GetComponent<InputField>().text = "";
                password.GetComponent<InputField>().text = "";
                text_error.GetComponent<Text>().text = "Login Sucsessful";
                text_error.GetComponent<Text>().enabled = true;
                print("Login Sucsessful");
                
                Application.LoadLevel("SampleScene");
            }
            

        }
    }

    public void toRegisterButton()
    {
        Application.LoadLevel("RegisterScreen");
    }

    public void LoginButton()
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
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Username != "" && Password != "")
            {
                LoginButton();
            }
        }

        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;

    }
}
