using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;

public class BaseCard : Item
{
    private Animation animation;
    private AnimationClip openAnimation;

    protected override void OnSpawn()
    {
        base.OnSpawn();
        GFX.sprite = backgroundSprite;
    }

    public async UniTask PlayOpenAnimation(GameplayConfig gameplayConfig)
    {
        Vector3 targetRotation = new Vector3(90, 0, 0);
        Vector3 lastRotation = new Vector3(0, 0, 0);
        Sequence dealSequence = Sequence.Create();
        dealSequence.Chain(Tween.Rotation(transform, transform.eulerAngles,targetRotation , gameplayConfig.cardOpenDuration * .5f, gameplayConfig.cardOpenEase)
            .OnComplete(() => UpdateGFX(defaultSprite)));
        dealSequence.Chain(Tween.EulerAngles(transform, transform.eulerAngles,lastRotation , gameplayConfig.cardOpenDuration * .5f, gameplayConfig.CardYMovementCurve));
        await dealSequence;
    }

    public async UniTask PlayDealAnimation(Vector3 targetPosition, GameplayConfig gameplayConfig)
    {
        Vector3 targetRotation = transform.eulerAngles + Vector3.forward * gameplayConfig.CardRotationAmaount;
        Sequence dealSequence = Sequence.Create();
        dealSequence.Group(Tween.EulerAngles(transform, transform.eulerAngles, targetRotation, gameplayConfig.CardDealMovementDuration, gameplayConfig.CardRotationEase));
        dealSequence.Group(Tween.PositionY(transform, targetPosition.y, gameplayConfig.CardDealMovementDuration, gameplayConfig.CardYMovementCurve));
        dealSequence.Group(Tween.PositionX(transform, targetPosition.x, gameplayConfig.CardDealMovementDuration,gameplayConfig.CardXMovementCurve));

        await dealSequence;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        // reset animation
    }
}