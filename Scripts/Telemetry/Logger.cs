using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
//using UnityEngine.Analytics;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class Logger : MonoBehaviour
{
    public string DirPath = "";
    public string LogPath = "";
    public string TouchLogPath = "";
    public string SurveyLogPath = "";
    public string DeviceLogPath = "";

    List<PlayerDeath> PlayerDeaths = new List<PlayerDeath>();
    List<PlayerJump> PlayerJumps = new List<PlayerJump>();
    List<PlayerAttack> PlayerAttacks = new List<PlayerAttack>();
    List<PlayerKill> PlayerKills = new List<PlayerKill>();
    List<PlayerGK> PlayerGKs = new List<PlayerGK>();
    List<FTouch> FTouches = new List<FTouch>();

    // Start is called before the first frame update
    void Start()
    {
        //string id = AnalyticsSessionInfo.userId;
        string id = SystemInfo.deviceUniqueIdentifier;
        
        DirPath = Application.persistentDataPath + "/" + id;
        CreateDirectory(DirPath);

        LogPath = DirPath + "/" + "GAME_LOG.txt";
        TouchLogPath = DirPath + "/" + "TOUCH_LOG.txt";
        SurveyLogPath = DirPath + "/" + "SURVEY_ANSWERS.txt";
        DeviceLogPath = DirPath + "/" + "DEVICE_INFO.txt";

        CreateLogFile(LogPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    void CreateLogFile(string path)
    {
        WriteToFile(path, "Dejavu3x LOG FILE, " + GetTime() + "\n");
    }

    void WriteToFile(string path, string content)
    {
        StreamWriter wr = new StreamWriter(path, false);
        wr.WriteLine(content);
        wr.Close();
    }

    void AppendToFile(string path, string delim=",", params string[] content)
    {
        StreamWriter wr = new StreamWriter(path, true);
        for(int i = 0; i < content.Length; i++)
        {
            wr.Write(content[i]);
            if (i != content.Length - 1)
                wr.Write(delim);
        }
        wr.Write("\n");
        wr.Close();
    }

    public string GetSystemTime()
    {
        return System.DateTime.UtcNow.ToString("yy-MM-dd-HH-mm-ss");
    }

    public string GetTime()
    {
        // return Time.timeSinceLevelLoad.ToString(); // OPTION 1
        return Time.time.ToString(); // OPTION 2
    }

    public void LogPlayerJump(Vector3 position, int controller)
    {
        string time = GetTime();
        PlayerJump newPlayerJump = new PlayerJump(position, controller);
        AppendToFile(LogPath, ",", "JUMP", time, position.x.ToString(), position.y.ToString(), controller.ToString());
    }

    public void LogPlayerDeath(Vector3 position, string trapName, int number, int controller)
    {
        string time = GetTime();
        PlayerDeath newPlayerDeath = new PlayerDeath(position, trapName, number, controller);
        AppendToFile(LogPath, ",", "DEATH", time, position.x.ToString(), position.y.ToString(), trapName, number.ToString(), controller.ToString());
    }

    public void LogPlayerAttack(Vector3 position, int controller)
    {
        string time = GetTime();
        PlayerAttack newPlayerAttack = new PlayerAttack(position, controller);
        AppendToFile(LogPath, ",", "ATTACK", time, position.x.ToString(), position.y.ToString(), controller.ToString());
    }

    public void LogPlayerKill(Vector3 position, int controller)
    {
        string time = GetTime();
        PlayerKill newPlayerKill = new PlayerKill(position, controller);
        AppendToFile(LogPath, ",", "KILL", time, position.x.ToString(), position.y.ToString(), controller.ToString());
    }

    public void LogPlayerGK(Vector3 position, int controller)
    {
        string time = GetTime();
        PlayerGK newPlayerGK = new PlayerGK(position, controller);
        AppendToFile(LogPath, ",", "GOTKEY", time, controller.ToString());
    }

    public void LogFTouch(Touch touch, string target, string time)
    {
        FTouch newFTouch = new FTouch(touch, target, time);
        //AppendToFile(LogPath, ",", "TOUCH", time, touch.fingerId.ToString(), touch.position.x.ToString(), touch.position.y.ToString(), controller.ToString());
        FTouches.Add(newFTouch);
        //Debug.Log("Add");
    }

    public void WriteTouchesLog()
    {
        CreateLogFile(TouchLogPath);
        foreach(FTouch ft in FTouches)
        {
            AppendToFile(TouchLogPath, ",", ft.time, ft.id.ToString(), ft.value, ft.x.ToString(), ft.y.ToString(), ft.controller.ToString());
        }
    }

    public void WriteDeviceInfo(string device, string os, string cpu, string ram, string gpu, string gtype, string w, string h)
    {
        CreateLogFile(DeviceLogPath);
        AppendToFile(DeviceLogPath, "\n", device, os, cpu, ram, gpu, gtype, w, h);
    }

    public void WriteSurveyAnswers(string[] answers)
    {
        CreateLogFile(SurveyLogPath);
        foreach(string ans in answers)
        {
            AppendToFile(TouchLogPath, ",", ans);
        }
    }

    public class PlayerJump
    {
        private Vector3 position;
        private int controller;

        public PlayerJump(Vector3 position, int controller)
        {
            this.position = position;
            this.controller = controller;
        }
    }

    public class PlayerDeath
    {
        private Vector3 position;
        private string trapName;
        private int number;
        private int controller;

        public PlayerDeath(Vector3 position, string trapName, int number, int controller)
        {
            this.position = position;
            this.trapName = trapName;
            this.number = number;
            this.controller = controller;
        }
    }

    public class PlayerAttack
    {
        private Vector3 position;
        private int controller;

        public PlayerAttack(Vector3 position, int controller)
        {
            this.position = position;
            this.controller = controller;
        }
    }

    public class PlayerKill
    {
        private Vector3 position;
        private int controller;

        public PlayerKill(Vector3 position, int controller)
        {
            this.position = position;
            this.controller = controller;
        }
    }

    public class PlayerGK
    {
        private Vector3 position;
        private int controller;

        public PlayerGK(Vector3 position, int controller)
        {
            this.position = position;
            this.controller = controller;
        }
    }

    public class FTouch
    {
        public string time;
        public int id;
        public string value;
        public float x;
        public float y;
        private float pressure;
        private float radius;
        public int controller;

        public FTouch(Touch touch, string value, string time)
        {
            this.time = time;
            this.id = touch.fingerId;
            this.value = value;
            this.x = touch.position.x;
            this.y = touch.position.y;
            this.pressure = touch.pressure;
            this.radius = touch.radius;
            this.controller = GameObject.Find("Game").GetComponent<AssignController>().controllerNum;
        }
    }
}