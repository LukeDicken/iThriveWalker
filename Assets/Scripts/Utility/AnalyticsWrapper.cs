/*
 * 
 * AnalyticsWrapper.cs -- A wrapper for Analytics calls
 * Created 2/18/17
 * 
 * Build and send analytics calls from here. Wrapped so that calls can be
 * standardised with playerIDs and whatnot here, rather than having to do
 * so on a per call basis. Also means that using UnityAnalytics becomes
 * swappable based on changing this single class rather than having a major
 * edit job.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsWrapper {

    public static void logCustomEvent(string EventName, IDictionary<string, System.Object> data)
    {
        // add additional info to the event data here
        Analytics.CustomEvent(EventName, data);
        LogWrapper.Log("Analytics: Recorded event " + EventName);
    }
}
