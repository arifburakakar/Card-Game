using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIGameplayHudPanel : UIBaseHudPanel
{
    [FormerlySerializedAs("BoardHandler")] public BoardProxy boardProxy;


    public void OnClickSet()
    {
       GameplayManager gameplayManager = (GameplayManager) GameControllerSystem.Instance.GameplayManager;
       gameplayManager.ActiveGame.SetSort();
    }

    public void OnClickRun()
    {
        GameplayManager gameplayManager = (GameplayManager) GameControllerSystem.Instance.GameplayManager;
        gameplayManager.ActiveGame.RunSort();
    }

    public void OnClickSmart()
    {
        GameplayManager gameplayManager = (GameplayManager) GameControllerSystem.Instance.GameplayManager;
        gameplayManager.ActiveGame.SmartSort();
    }

    public void OnClickLoadMeta()
    {
        GameControllerSystem.Instance.LoadMeta();
    }
}
