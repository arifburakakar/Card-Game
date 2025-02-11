using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UITransitionHandler : MonoBehaviour
{
    [SerializeField] 
    private GameObject transitionContainer;
    [SerializeField] 
    private Animation animation;
    [SerializeField] 
    private AnimationClip fadeInClip;
    [SerializeField] 
    private AnimationClip fadeOutClip;
    
    public async UniTask FadeIn()
    {
        transitionContainer.SetActive(true);
        animation.Play(fadeInClip.name);
        await Yield.WaitForSeconds(fadeInClip.length);
    }

    public void ResetTransition()
    {
        transitionContainer.SetActive(false);
    }

    public async UniTask FadeOut()
    {
        transitionContainer.SetActive(true);
        animation.Play(fadeOutClip.name);
        await Yield.WaitForSeconds(fadeOutClip.length);
        ResetTransition();
    }
}
