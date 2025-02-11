using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIGameplayHudPanel : UIBaseHudPanel
{
    [FormerlySerializedAs("BoardHandler")] public BoardProxy boardProxy;
}
