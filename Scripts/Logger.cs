using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Logger {
    // Master switch
    public static bool EnableLogs = true;

    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Log(object message) {
        if (!EnableLogs) return;
        UnityEngine.Debug.Log(message);
    }


    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Warning(object message) {
        if (!EnableLogs) return;
        UnityEngine.Debug.LogWarning(message);
    }
    [Conditional("UNITY_EDITOR")]
    [Conditional("DEVELOPMENT_BUILD")]
    public static void Error(object message) {
        if (!EnableLogs) return;
        UnityEngine.Debug.LogError(message);
    }



    
}