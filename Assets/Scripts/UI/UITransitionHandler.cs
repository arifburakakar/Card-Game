using System.Collections;
using System.Collections.Generic;
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
    
    public float FadeIn()
    {
        transitionContainer.SetActive(true);
        animation.Play(fadeInClip.name);
        return fadeInClip.length;
    }

    public float FadeOut()
    {
        transitionContainer.SetActive(true);
        animation.Play(fadeOutClip.name);
        return fadeOutClip.length;
    }
}
