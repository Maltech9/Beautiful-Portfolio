using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using ORKFramework;



public class TimeScript : MonoBehaviour, ISaveData {


  private Text textClock;

  [SerializeField] private int delay = 2;

    public GameObject dayvolume;

    public GameObject nightvolume;

    public GameObject winterSpawn;

    public GameObject springSpawn;

    public GameObject summerSpawn;

    public GameObject fallSpawn;

    public Canvas uiCanvas;

    int minute = 30;

    int hour = 8;

    int day = 1;

    int monthint = 4;

    int daycount = 1;



    void Awake (){

    textClock = GetComponent<Text>();

     StartCoroutine(Time());

  }



    void Start()
    {
        ORK.SaveGame.RegisterCustomData("test", this, false);
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // called when a game is saved
    public DataObject SaveGame()
    {
        DataObject data = new DataObject();

        data.Set("minute", this.minute);
        data.Set("hour", this.hour);
        data.Set("day", this.day);
        data.Set("Monthint", this.monthint);
        data.Set("daycount", this.daycount);

        return data;
    }

    // called when a game is loaded
    // depending on the 'beforeSceneLoad' parameter of the registration, 
    // the function will be called either before or after loading the scene.
    public void LoadGame(DataObject data)
    {
        if (data != null)
        {
            data.Get("minute",ref this.minute);
            data.Get("hour", ref this.hour);
            data.Get("day", ref this.day);
            data.Get("Monthint", ref this.monthint);
            data.Get("daycount", ref this.daycount);
        }
    }



IEnumerator Time(){

    DateTime time = DateTime.Now;


    string month = "January";

        string season = "nigga";
        string AmPm = "Nigga";
        int displayhour = 0;
        string dayofweek = "Nigga";

   
        //Will loop infinitely 
        while (true)
        {
            //wait the time of the delay
            yield return new WaitForSeconds(delay);
            //process time
            minute++;

            if(minute == 60)
            {
                hour++;
                minute = 0;
            }

            if (hour == 24)
            {
                day++;
                daycount++;
                hour = 0;
            }
            
            if (day == 8)
            {
                day = 1;
                
            }

            //Handle the day of the week
            if (day == 1)
            {
                dayofweek = "Monday";
            }

            if (day == 2)
            {
                dayofweek = "Tuesday";
            }

            if (day == 3)
            {
                dayofweek = "Wednesday";
            }

            if (day == 4)
            {
                dayofweek = "Thursday";
            }

            if (day == 5)
            {
                dayofweek = "Friday";
            }

            if (day == 6)
            {
                dayofweek = "Saturday";
            }

            if (day == 7)
            {
                dayofweek = "Sunday";
            }


            //more processing
            if (daycount == 30)
            {
                monthint++;
            }
            if (hour >= 20 || hour <= 6)
            {
                dayvolume.SetActive(false);
                nightvolume.SetActive(true);
            }
            else
            {
                dayvolume.SetActive(true);
                nightvolume.SetActive(false);
            }
            switch (monthint)
            {
                case 1:
                    month = "January";
                    break;

                case 2:
                    month = "February";
                    break;

                case 3:
                    month = "March";
                    break;

                case 4:
                    month = "April";
                    break;

                case 5:
                    month = "May";
                    break;

                case 6:
                    month = "June";
                    break;

                case 7:
                    month = "July";
                    break;

                case 8:
                    month = "August";
                    break;

                case 9:
                    month = "September";
                    break;

                case 10:
                    month = "October";
                    break;

                case 11:
                    month = "November";
                    break;

                case 12:
                    month = "December";
                    break;

            }
            if (monthint >= 1 && monthint <= 3)
            {
                season = "Winter";
                winterSpawn.SetActive(true);
                springSpawn.SetActive(false);
                summerSpawn.SetActive(false);
                fallSpawn.SetActive(false);
            }
            if (monthint >= 4 && monthint <= 6)
            {
                season = "Spring";
                winterSpawn.SetActive(false);
                springSpawn.SetActive(true);
                summerSpawn.SetActive(false);
                fallSpawn.SetActive(false);
            }
            if (monthint >= 7 && monthint <= 9)
            {
                season = "Summer";
                winterSpawn.SetActive(false);
                springSpawn.SetActive(false);
                summerSpawn.SetActive(true);
                fallSpawn.SetActive(false);
            }
            else if (monthint >= 10 && monthint <= 12)
            {
                season = "Fall";
                winterSpawn.SetActive(false);
                springSpawn.SetActive(false);
                summerSpawn.SetActive(false);
                fallSpawn.SetActive(true);
            }
            if(hour > 11){

                displayhour = hour - 12;

                AmPm = "PM";

            }
            else
            {
                displayhour = hour;
                AmPm = "AM";
            }

            this.GetComponent<Text>().text = displayhour + ":" + LeadingZero(minute) + " " + AmPm +" \n"+ dayofweek + ", "  + month + " "+ LeadingZero(daycount) +",\n --- " + season;
        }

    }
string LeadingZero (int n){
     return n.ToString().PadLeft(2, '0');
  }
}