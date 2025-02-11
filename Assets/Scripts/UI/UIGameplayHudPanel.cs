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
        GameControllerSystem.Instance.GetGame().SetSort();
    }

    public void OnClickRun()
    {
        GameControllerSystem.Instance.GetGame().RunSort();

    }

    public void OnClickSmart()
    {
        GameControllerSystem.Instance.GetGame().SmartSort();
    }
}
