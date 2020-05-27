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

public class CloudContentManager : MonoBehaviour
{

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
    private string target = "1";
    private string new_model = "";
    private string data;
    private String[] Lines_data;

    readonly string[] starRatings = { "☆☆☆☆☆", "★☆☆☆☆", "★★☆☆☆", "★★★☆☆", "★★★★☆", "★★★★★" };

    Dictionary<string, GameObject> Augmentations;
    Transform contentManagerParent;
    Transform currentAugmentation;

    IEnumerator Upload()
    {
        if(new_model == "charmander"){
            new_model = "squirtle";
            Debug.Log("CHAR - SQRT");
        }
        else{
            new_model = "charmander";
            Debug.Log("SQRT - CHAR");
        }

        JsonClass form = new JsonClass();
        form.model = target;
        form.name = new_model;
        string json = JsonUtility.ToJson(form);
        Debug.Log("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + target);
        Debug.Log(new_model);
        Debug.Log(json);
        using (UnityWebRequest www = UnityWebRequest.Put("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + target, json))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
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
            new_model = "";
            data = uwr.downloadHandler.text;
            Lines_data = data.Split(',');
            Debug.Log("DATA: " + Lines_data.Length);
            //Debug.Log("model: " + Lines_data[0]);
            //Debug.Log("Password: " + Lines_data[1]);
            
            
            if (Lines_data.Length == 2) //api check
            {
                new_model = Lines_data[0].Split(':')[1];
                new_model = new_model.Trim('"','}');
                Debug.LogWarning(new_model);
                Debug.LogWarning(target);
                
                GameObject augmentation = GetValuefromDictionary(Augmentations, new_model);
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

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS


    void Start()
    {
        Augmentations = new Dictionary<string, GameObject>();

        for (int a = 0; a < AugmentationObjects.Length; ++a)
        {
            Augmentations.Add(AugmentationObjects[a].targetName,
                              AugmentationObjects[a].augmentation);
        }
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void ChangePokemon(string Pokemon)
    {
        StartCoroutine(Upload());
        Debug.Log("Change pokemon");
    }

    public void ShowTargetInfo(bool showInfo)
    {
        Canvas canvas = cloudTargetInfo.GetComponentInParent<Canvas>();

        canvas.enabled = showInfo;
    }

    public void HandleTargetFinderResult(Vuforia.TargetFinder.CloudRecoSearchResult targetSearchResult)
    {
        Debug.Log("<color=blue>HandleTargetFinderResult(): " + targetSearchResult.TargetName + "</color>");
        target = targetSearchResult.TargetName;
        cloudTargetInfo.text =
            "Name: " + targetSearchResult.TargetName +
            "\nRating: " + starRatings[targetSearchResult.TrackingRating] +
            "\nMetaData: " + ((targetSearchResult.MetaData.Length > 0) ? targetSearchResult.MetaData : "No") +
            "\nTarget Id: " + targetSearchResult.UniqueTargetId;

        StartCoroutine(GetRequest("https://quiet-crag-61602.herokuapp.com/all-pokemons/" + targetSearchResult.TargetName));
        
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
