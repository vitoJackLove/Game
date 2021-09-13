using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debuger
{
    public static void Log(object obj)
    {
#if IDEBUG
        Debug.Log(obj);
#endif
    }

    public static void LogError(object obj)
    {
#if IDEBUG
        Debug.LogError(obj);
#endif
    }

    public static void LogWarning(object obj)
    {
#if IDEBUG
        Debug.LogWarning(obj);
#endif
    }
}

