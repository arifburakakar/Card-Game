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

    public UniTask PlayOpenAnimation()
    {
        return Yield.WaitForSeconds(1);
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