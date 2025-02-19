using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;

public class BaseCard : Item
{
    [SerializeField]
    private Animation animation;
    [SerializeField]
    private AnimationClip flipInAnimation;
    [SerializeField]
    private AnimationClip flipOutAnimation;
    [SerializeField]
    private AnimationClip rotateAnimation;
    private Tween tween;
    [SerializeField] public float selectScale = 1.45f;
    [SerializeField] public float scaleDuration = .10f;

    protected override void OnSpawn()
    {
        base.OnSpawn();
        GFX.sprite = backgroundSprite;
    }

    public override void Select()
    {
        base.Select();
        ScaleAnimation(selectScale, scaleDuration);
    }

    public override void Deselect()
    {
        base.Deselect();
        ScaleAnimation(1, scaleDuration);
    }

    public async UniTask PlayOpenAnimation()
    {
        animation.Play(flipInAnimation.name);
        await Yield.WaitForSeconds(flipInAnimation.length);
        GFX.sprite = defaultSprite;
        animation.Play(flipOutAnimation.name);
        await Yield.WaitForSeconds(flipOutAnimation.length);
    }
    public async UniTask PlayDealAnimation(Vector3 targetPosition, GameplayConfig gameplayConfig)
    {
        animation.Play(rotateAnimation.name);
        Sequence dealSequence = Sequence.Create();
        dealSequence.Group(Tween.PositionY(transform, targetPosition.y, gameplayConfig.CardDealMovementDuration,
            gameplayConfig.CardYMovementCurve));
        dealSequence.Group(Tween.PositionX(transform, targetPosition.x, gameplayConfig.CardDealMovementDuration,
            gameplayConfig.CardXMovementCurve));

        await dealSequence;
    }

    protected override void OnDespawn()
    {
        base.OnDespawn();
        SetScale(Vector3.one);
    }
}