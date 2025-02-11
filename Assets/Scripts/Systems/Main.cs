using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main Instance;
    
    public Action MainUpdate;
    public Action MainFixedUpdate;
    public Action MainLateUpdate;
    
    public string MetaSceneName = "Meta Scene";
    public string GameplaySceneName = "Gameplay Scene";

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        SetTargetFrameRate();
        SetGCToManuel();
        CreateInstances();
    }

    private void Start()
    {
        InitializeInstances();
        StartGame();
    }

    private void Update()
    {
        MainUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        MainFixedUpdate?.Invoke();
    }

    private void LateUpdate()
    {
        MainLateUpdate?.Invoke();
    }

    private void SetTargetFrameRate()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 120;
    }

    private void SetGCToManuel()
    {
        GameUtility.GCCollectManuel();
    }
    
    private void CreateInstances()
    {
        // async or uniTask can be use
        GameObject coroutineHandlerObject = new GameObject("CoroutineHandler");
        CoroutineHandler coroutineHandler = coroutineHandlerObject.AddComponent<CoroutineHandler>();
        coroutineHandler.OnCreate();
        
        GameControllerSystem.CreateInstance();
        UIControllerSystem.CreateInstance();
    }

    private void InitializeInstances()
    {
        GameControllerSystem.Instance.Initialize();
        UIControllerSystem.Instance.Initialize();
    }

    private void StartGame()
    {
        GameControllerSystem.Instance.LoadMain();
    }
}