using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;
    private string Username;
    private string Password;
    private String[] Lines;
    private string DecryptedPass;

    public void toRegisterButton()
    {
        Application.LoadLevel("RegisterScreen");
    }

    public void LoginButton()
    {
        bool UN = false;
        bool PW = false;
        if(Username != "")
        {
            if(System.IO.File.Exists(@"C:/Anton/UnityTest/" + Username + ".txt")) //api check
            {
                UN = true;
                Lines = System.IO.File.ReadAllLines(@"C:/Anton/UnityTest/" + Username + ".txt"); //api get login and pass 
            }
            else
            {
                Debug.LogWarning("Username Invalid");
            }
        }
        else
        {
            Debug.LogWarning("Username Field Empty");
        }
        if(Password != "")
        {
            if (System.IO.File.Exists(@"C:/Anton/UnityTest/" + Username + ".txt"))
            {
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
                    Debug.LogWarning("Password is Invalid");
                }
            }
            else
            {
                Debug.LogWarning("Password is Invalid");
            }
        }
        else
        {
            Debug.LogWarning("Password Field Empty");
        }
        if (UN == true && PW == true)
        {
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            print("Login Sucsessful");
            Application.LoadLevel("SampleScene");
        }
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
