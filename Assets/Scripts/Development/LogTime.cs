using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogTime {
    public static string Time() {
        float time = UnityEngine.Time.time;
        var hh = Mathf.FloorToInt(time / 3600).ToString("00");
        var mm = Mathf.FloorToInt((time % 3600) / 60).ToString("00");
        var ss = Mathf.FloorToInt((time % 3600) % 60).ToString("00");
        var ms = Mathf.FloorToInt((time - Mathf.FloorToInt(time)) * 1000);

        var timeString = hh+":"+mm+":"+ss+"."+ms;
        return timeString;
    }
}
