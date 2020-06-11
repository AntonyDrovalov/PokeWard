/*===============================================================================
Copyright (c) 2017-2018 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using static Globals;

public class CloudContentManager : MonoBehaviour
{

    public string target;
    public string new_model;
    public string data;
    public String[] Lines_data;
    private bool anim;

    #region PRIVATE_MEMBER_VARIABLES

    [SerializeField] Transform CloudTarget = null;
    [SerializeField] UnityEngine.UI.Text cloudTargetInfo = null;

    [System.Serializable]
    public class AugmentationObject
    {
        public string targetName;
        public GameObject augmentation;
    }

    private class JsonClass
    {
        public string model;
        public string name;
    }

    public AugmentationObject[] AugmentationObjects;
    //public Animation anim1;
    //public Animation anim2;

    readonly string[] starRatings = { "☆☆☆☆☆", "★☆☆☆☆", "★★☆☆☆", "★★★☆☆", "★★★★☆", "★★★★★" };

    Dictionary<string, GameObject> Augmentations;
    Transform contentManagerParent;
    Transform currentAugmentation;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS


    void Start()
    {
        Debug.Log("USERNAME STORED = " + Globals.Global_user);
        Augmentations = new Dictionary<string, GameObject>();

        for (int a = 0; a < AugmentationObjects.Length; ++a)
        {
            Augmentations.Add(AugmentationObjects[a].targetName,
                              AugmentationObjects[a].augmentation);
            //AugmentationObjects[a].augmentation.GetComponent<Animator>().enabled = false;
        }
        anim = false;
        //AugmentationObjects[0].augmentation.GetComponent<Animator>().enabled = false;
        //AugmentationObjects[1].augmentation.GetComponent<Animator>().enabled = false;
        Debug.Log("Aug: " + Augmentations);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    IEnumerator Upload()
    {
        if(Globals.Global_model == "charmander"){
            Globals.Global_model = "squirtle";
            Debug.Log("CHAR - SQRT");
        }
        else{
            Globals.Global_model = "charmander";
            Debug.Log("SQRT - CHAR");
        }

        var uwr = new UnityWebRequest("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + Globals.Global_target, "PUT");

        JsonClass form = new JsonClass();
        form.model = Globals.Global_target;
        form.name = Globals.Global_model;
        string json = JsonUtility.ToJson(form);
        Debug.Log("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + Globals.Global_target);
        Debug.Log(Globals.Global_model);
        Debug.Log(json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
        //Debug.Log(myData.Length);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            //Debug.Log("My data=" + myData);
            Debug.Log("Upload complete!");
        }
        
    }

    IEnumerator Upload_charmander()
    {

        var uwr = new UnityWebRequest("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + Globals.Global_target, "PUT");

        JsonClass form = new JsonClass();
        form.model = Globals.Global_target;
        form.name = "charmander";
        string json = JsonUtility.ToJson(form);
        Debug.Log("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + Globals.Global_target);
        Debug.Log(Globals.Global_model);
        Debug.Log(json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
        //Debug.Log(myData.Length);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            //Debug.Log("My data=" + myData);
            Debug.Log("Upload complete!");
        }  
    }

    IEnumerator Upload_squirtle()
    {

        var uwr = new UnityWebRequest("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + Globals.Global_target, "PUT");

        JsonClass form = new JsonClass();
        form.model = Globals.Global_target;
        form.name = "squirtle";
        string json = JsonUtility.ToJson(form);
        Debug.Log("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + Globals.Global_target);
        Debug.Log(Globals.Global_model);
        Debug.Log(json);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
        //Debug.Log(myData.Length);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            //Debug.Log("My data=" + myData);
            Debug.Log("Upload complete!");
        }  
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
            Globals.Global_model = "";
            data = uwr.downloadHandler.text;
            Lines_data = data.Split(',');
            Debug.Log("DATA: " + Lines_data.Length);
            //Debug.Log("model: " + Lines_data[0]);
            //Debug.Log("Password: " + Lines_data[1]);
            
            
            if (Lines_data.Length == 2) //api check
            {
                Globals.Global_model = Lines_data[0].Split(':')[1];
                Globals.Global_model = Globals.Global_model.Trim('"','}');
                Debug.LogWarning(Globals.Global_model);
                Debug.LogWarning(Globals.Global_target);
                
                GameObject augmentation = GetValuefromDictionary(Augmentations, Globals.Global_model);
                if (augmentation != null)
                {
                    if (augmentation.transform.parent != CloudTarget.transform)
                    {
                        Renderer[] augmentationRenderers;

                        if (currentAugmentation != null && currentAugmentation.parent == CloudTarget)
                        {
                            currentAugmentation.SetParent(contentManagerParent);
                            currentAugmentation.transform.localPosition = Vector3.zero;

                            augmentationRenderers = currentAugmentation.GetComponentsInChildren<Renderer>();
                            foreach (var objrenderer in augmentationRenderers)
                            {
                                objrenderer.gameObject.layer = LayerMask.NameToLayer("UI");
                                objrenderer.enabled = true;
                            }
                        }

                        // store reference to content manager's parent object of the augmentation
                        contentManagerParent = augmentation.transform.parent;
                        // store reference to the current augmentation
                        currentAugmentation = augmentation.transform;

                        // set new target augmentation parent to cloud target
                        augmentation.transform.SetParent(CloudTarget);
                        augmentation.transform.localPosition = Vector3.zero;

                        augmentationRenderers = augmentation.GetComponentsInChildren<Renderer>();
                        foreach (var objrenderer in augmentationRenderers)
                        {
                            objrenderer.gameObject.layer = LayerMask.NameToLayer("Default");
                            objrenderer.enabled = true;
                        }

                    }
                } 
            }
            else
            {
                
                Debug.LogWarning("Invalid req");
            }   
        }
    }

    public void ChangePokemon()
    {   
        Application.LoadLevel("SelectionScene");
        //Debug.Log(targetSearchResult);
    }

    public void BackButton(){
        Application.LoadLevel("SampleScene");
    }

    public void PlayButton(){
        GameObject augmentation = GetValuefromDictionary(Augmentations, Globals.Global_model);
        
        augmentation.GetComponent<Animator>().enabled = false;
        //anim.Stop();
        
        //else{
        //    augmentation.GetComponent<Animator>().enabled = false;
            //anim.Play();
        

    }

    public void ChangePokemonCharmander()
    {   
        StartCoroutine(Upload_charmander());
        Application.LoadLevel("SampleScene");
        //Debug.Log(targetSearchResult);
    }

    public void ChangePokemonSquirtle()
    {   
        StartCoroutine(Upload_squirtle());
        Application.LoadLevel("SampleScene");
        //Debug.Log(targetSearchResult);
    }

    public void ShowTargetInfo(bool showInfo)
    {
        Canvas canvas = cloudTargetInfo.GetComponentInParent<Canvas>();

        canvas.enabled = showInfo;
    }

    public void HandleTargetFinderResult(Vuforia.TargetFinder.CloudRecoSearchResult targetSearchResult)
    {
        Globals.Global_target = targetSearchResult.TargetName;
        target = targetSearchResult.TargetName;
        Debug.Log("<color=blue>HandleTargetFinderResult(): " + target + "</color>");
        
        Debug.Log("TARGET1 = " + target);
        

        StartCoroutine(GetRequest("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + target));
        Debug.Log("TARGET2 = " + target);
        cloudTargetInfo.text =
            "Target: " + target +
            "\nPokemon: " + Globals.Global_model;
        Debug.Log("TARGET GLOBAL = " + Globals.Global_target);
    }

    #endregion // PUBLIC_METHODS


    #region // PRIVATE_METHODS

    GameObject GetValuefromDictionary(Dictionary<string, GameObject> dictionary, string key)
    {
        Debug.Log("<color=blue>GetValuefromDictionary() called.</color>");
        if (dictionary == null)
            Debug.Log("dictionary is null");

        if (dictionary.ContainsKey(key))
        {
            Debug.Log("key: " + key);
            GameObject value;
            dictionary.TryGetValue(key, out value);
            Debug.Log("value: " + value.name);
            return value;
        }

        return null;
        //return "Key not found.";
    }

    #endregion // PRIVATE_METHODS
}
