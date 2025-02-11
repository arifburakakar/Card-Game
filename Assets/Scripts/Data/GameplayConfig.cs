using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;

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
    public float cardOpenDuration = .20f;
    public Ease cardOpenEase = Ease.Linear;

    [Header("Deal Animation")] 
    public float DealCardTargetOffsetHeight = 1;
    public float CardDealMovementDuration = 1;
    public float CardRotationAmaount = 4;
    public Ease CardRotationEase;
    public AnimationCurve CardXMovementCurve;
    public AnimationCurve CardYMovementCurve;
}