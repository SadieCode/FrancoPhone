using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLogic : MonoBehaviour {

    public Light lightSource;
    public Text txtTime;
    static float min = 0;
    public static int hour = 18;
    public static int day = 2;
    public static int weekDay = 6; //1 = sunday, 2 = monday... so on
    public static int month = 11;
    public static int firstDayofMonth = 5;
    float secondsInMin = 1f;
    public static bool stop = false;

    public GameObject calendarPanel;
    public GameObject calendar;
    public GameObject daysPanel;
    public Text monthLabel;
    Text[] dayLabels;


    Color currentDayColor = new Color32(128, 255, 128, 100);

    Text[] calendarDays;
    Image[] calendarDayBoxes;

    //0 = english, 1 = french
    public static int mode = 1;

    string[] monthsFr = {"janvier","février","mars","avril","mai","juin","juillet",
        "août","septembre","octobre","novembre","décembre"};
    string[] monthsEng = { "January", "February","March","April","May","June","July",
        "August","September","October","November","December"};
    int[] daysPerMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    string[] weekDaysFr = { "dimanche", "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi" };
    string[] weekDaysEng = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

    // Use this for initialization
    void Start () {
        calendarDays = calendar.GetComponentsInChildren<Text>();
        calendarDayBoxes = calendar.GetComponentsInChildren<Image>();
        dayLabels = daysPanel.GetComponentsInChildren<Text>();
        RedrawCalendar();
	}
	
	// Update is called once per frame
	void Update () {

        if (!stop)
        {
            min += Time.deltaTime * secondsInMin;
            if (min >= 60)
            {
                min = 0;
                hour++;
            }
            if (hour >= 24)
            {
                hour = 0;
                day++;
                weekDay++;
            }
            if(weekDay > 7)
            {
                weekDay = 1;
            }
            if(day > daysPerMonth[month - 1])
            {
                day = 1;
                month++;
                firstDayofMonth = weekDay;
                RedrawCalendar();
            }
            if(month > 12)
            {
                month = 1;
            }

            UpdateLight();
            txtTime.text = hour.ToString().PadLeft(2, '0') + ":" + ((int)min).ToString().PadLeft(2, '0') +
                " " + day +"/" + month;
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

    public void OpenCalendar()
    {
        foreach (Image dayBox in calendarDayBoxes)
        {
            dayBox.color = Color.white;
        }
        calendarDayBoxes[day + firstDayofMonth - 1].color = currentDayColor;
        calendarPanel.SetActive(true);
    }

    public void CloseCalendar()
    {
        calendarPanel.SetActive(false);
    }

    public void RedrawCalendar()
    {
        if (mode == 0)
        {
            monthLabel.text = monthsEng[month - 1];
            for (int i = 0; i < 7; i++)
            {
                dayLabels[i].text = weekDaysEng[i];
            }
        }
        else
        {
            monthLabel.text = monthsFr[month - 1];
            for (int i = 0; i < 7; i++)
            {
                dayLabels[i].text = weekDaysFr[i];
            }
        }

        foreach (Text txt in calendarDays)
        {
            txt.text = null;
        }

        int n = 1;
        for(int i = firstDayofMonth - 1; i < daysPerMonth[month - 1] + (firstDayofMonth - 1); i++)
        {
            calendarDays[i].text = n.ToString();
            n++;
        }
    }

    public void ChangeMode()
    {
        mode = 1;
        RedrawCalendar();
    }
}
