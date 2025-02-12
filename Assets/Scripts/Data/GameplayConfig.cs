using PrimeTween;
using UnityEngine;

[CreateAssetMenu(fileName = "Gameplay Config", menuName = "GameplayConfig", order = 0)]
public class GameplayConfig : ScriptableObject
{
    [Header("Board")] 
    public GameObject DealerDeck;
    
    [Header("Hand")] 
    public float HandWidth = 30;
    public float HandYPosition = 300;
    public float HandArcAngle = 60;
    public float RotationMultiplier = 30;
    public int DefaultHandCount = 11;
    public float CardSlotMovementSpeed = 10;
    public float DragSpeed = 100;
    public float SelectHeightOffset = .75f;

    [Header("Deal Animation")] 
    public float DealCardTargetOffsetHeight = 1;
    public float CardDealMovementDuration = 1;
    public AnimationCurve CardXMovementCurve;
    public AnimationCurve CardYMovementCurve;
}