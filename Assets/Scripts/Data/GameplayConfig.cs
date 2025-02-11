using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Gameplay Config", menuName = "GameplayConfig", order = 0)]
public class GameplayConfig : ScriptableObject
{
    [FormerlySerializedAs("boardHandler")] [Header("Board")] 
    public BoardProxy boardProxy;
    
    [Header("Hand")] 
    public float HandWidth = 30;
    public float HandYPosition = 300;
    public int DefaultHandCount = 11;

    [Header("Card Animation")] 
    public float CardDealMovementDuration = 1;
    public float CardRotationAmaount = 4;
    public Ease CardRotationEase;
    public AnimationCurve CardXMovementCurve;
    public AnimationCurve CardYMovementCurve;
}