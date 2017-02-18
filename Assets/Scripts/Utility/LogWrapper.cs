/*
 * 
 * Logger.cs -- A Debug.Log() / Debug.Error() wrapper.
 * Created 2/18/17
 * 
 * Why a wrapper? Because then from one place we can change the log/error
 * behaviour, enforce warn levels etc. Also means that longer term things might
 * get redirected so instead of using the Unity Debug system, we could output
 * to a text file or to something like splunk - by maintaining this one class
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LogWrapper { // does not need to inherit from MonoBehaviour

    private static bool suppressErrors = false;

	public static void Log(string message)
    {
        Debug.Log(message);
    }

    public static void Error(string message)
    {
        if(suppressErrors)
        {
            Log("Suppressed Error: " + message);
        }
    }
}
