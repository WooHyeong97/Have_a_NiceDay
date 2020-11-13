﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public GameData gameData;

    // 게임매니저
    #region 싱글톤(게임매니저)
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<GameManager>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newobj = new GameObject().AddComponent<GameManager>();
                    instance = newobj;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType<GameManager>();
        if(objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion 싱글톤(게임매니저)

    private void Start()
    {
        //LoadGameDataFromJson();
        currentStageNumber = gameData.StageNumber;
        currentStationNumber = gameData.StationNumber;
    }

    // JSON 데이터 save&load
    #region JSON

    // 데이터 저장 함수
    public void SaveGameDataToJson()
    {
        string jsonData = JsonUtility.ToJson(gameData, true);
        string path = Path.Combine(Application.dataPath, "GameData.json");
        File.WriteAllText(path, jsonData);
    }

    // 데이터 로드 함수
    public void LoadGameDataFromJson()
    {
        string path = Path.Combine(Application.dataPath, "GameData.json");
        string jsonData = File.ReadAllText(path);
        gameData = JsonUtility.FromJson<GameData>(jsonData);
    }

    #endregion JSON


    // 임시로 사용할 역과 스테이지 관련 변수와 함수들
    #region 현재 게임 스테이지 정보를 담은 임시용
    public int currentStationNumber;
    public int currentStageNumber;

    public void GetScore()
    {
        if (gameData.StationNumber <= 10 && gameData.StageNumber < 4)
        {
            gameData.StageNumber += 1;
            currentStageNumber = gameData.StageNumber;
            SaveGameDataToJson();

            Debug.Log(" 다음 스테이지로 이동합니다. 현재 " + gameData.StationNumber
                + " 번째 역의 " + gameData.StageNumber + "스테이지 입니다."); // 출력한번 해주고
            //LoadGameDataFromJson();
        }
        else if (gameData.StationNumber <= 10 && gameData.StageNumber == 4)
        {
            if (gameData.StationNumber == 10)
            {
                Debug.Log("ALL STATION CLEAR!!");
                return;
            }
            else
            {
                gameData.StageNumber = 1;
                gameData.StationNumber += 1;
                currentStageNumber = gameData.StageNumber;
                currentStationNumber = gameData.StationNumber;
                SaveGameDataToJson();

                Debug.Log(" 모든 스테이지 클리어 다음 역인 " + gameData.StationNumber
                    + " 번째 역의 " + gameData.StageNumber + "스테이지로 이동합니다."); // 출력한번 해주고
                //LoadGameDataFromJson();
            }
        }
    }
    #endregion 현재 게임 스테이지 정보를 담은 임시용
}

[System.Serializable]
public class GameData // 게임 데이타 저장용 클래스 (추후에 수정)
{
    public int StageNumber = 1; // 스테이지는 1 부터 4까지
    public int StationNumber = 1;
}

[System.Serializable]
public class CharacterIllust
{
    public bool isSetting = false;

    public int AccumulateNumber = 1;
    public int studentNumber = 0;
    public int workerNumber = 0;
    public int developerNumber = 0;
    public int soldierNumber = 0;

    public GameObject[] studentButtons;
    public GameObject[] workerButtons;
    public GameObject[] developerButtons;
    public GameObject[] soldierButtons;
}