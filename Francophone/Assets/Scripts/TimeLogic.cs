using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLogic : MonoBehaviour {

    public Light lightSource;
    public Text txtTime;
    float min = 0;
    public static int hour = 18;
    float secondsInMin = 5f;
    public static bool stop = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(stop);
        if (!stop)
        {
            min += Time.deltaTime * secondsInMin;
            //Debug.Log(min.ToString());
            if (min >= 60)
            {
                min = 0;
                hour++;
            }
            if (hour >= 24)
            {
                hour = 0;
            }
            UpdateLight();
            txtTime.text = hour.ToString().PadLeft(2, '0') + ":" + ((int)min).ToString().PadLeft(2, '0');
        }
    }

    void UpdateLight()
    {
        if(hour >= 18 && hour <= 21)
        {
            if (lightSource.intensity > 0.1)
            {
                lightSource.intensity -= Time.deltaTime/200.0f;
            }
        }

        if (hour >= 6 && hour <= 9)
        {
            if (lightSource.intensity < 0.9)
            {
                lightSource.intensity += Time.deltaTime/200.0f;
            }
        }
    }

    public void setTime(float time)
    {

    }

    public int getTime()
    {
        return hour;
    }
}
