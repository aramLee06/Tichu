using UnityEngine;
using DynamicMusicSystem;
using System.Collections.Generic;

/// <summary>
/// Controller class of DynaMusic. Contains information about the dynamic music map.
/// </summary>
public class MusicController : ScriptableObject
{
    public List<DynaMusicLayer> layers; //List containing the various music layers.
    public ulong currentLayerUUID = 0; //UUID for transition targeting.
    public Dictionary<string, float> parameters = new Dictionary<string, float>(); //The paramters, used for conditions.

    #if UNITY_EDITOR
    public Texture2D icon;
    #endif

    [SerializeField] private List<string> keyList; //List of parameter keys, since dictionaries don't get serialized
    [SerializeField] private List<float> valueList; //List of parameter values.

    public MusicController()
    {
        layers = new List<DynaMusicLayer>() {  };
        layers.Add(new DynaMusicLayer(currentLayerUUID));
        currentLayerUUID += 2000;
    }

    /// <summary>
    /// Loads important data regarding the class into easily-accessible variables. Definitely needs cleaning up.
    /// </summary>
    public void LoadClass()
    {
        parameters.Clear();
        if (keyList != null && valueList != null)
        {
            for (int i = 0; i < keyList.Count; i++)
            {
                parameters.Add(keyList[i], valueList[i]);
            }
        }
    }

    /// <summary>
    /// Saves important data regarding the class from easily-accessible variables into serializable ones. Probably needs cleaning up.
    /// </summary>
    public void SaveClass() //Saves all important data.
    {
        keyList = new List<string>(parameters.Keys);
        valueList = new List<float>(parameters.Values);
    }

    /// <summary>
    /// Safely set a parameter value. Redundant as it's already done via the Handler.
    /// </summary>
    /// <param name="name">Name of the parameter.</param>
    /// <param name="value">The new value of the parameter</param>
    /// <returns></returns>
    public bool SetValue(string name, float value) 
    {
        if(parameters.ContainsKey(name))
        {
            parameters[name] = value;
            return true;
        }
        return false;
    }
}