using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;
using System;

public static class AudioPreview
{
    public static void PlayClip(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod( "PlayClip", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(AudioClip) }, null);
        method.Invoke(null, new object[] { clip });
    }

    public static void StopAllClips()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod( "StopAllClips", BindingFlags.Static | BindingFlags.Public, null, new Type[] { }, null);
        method.Invoke( null, new object[] { });
    }
}