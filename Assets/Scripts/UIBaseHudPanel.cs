using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBaseHudPanel : MonoBehaviour , IUIHudPanel
{
    public Canvas Canvas;
    public UITransitionHandler TransitionHandler;
    public void Create(Transform rootTransform)
    {
        transform.SetParent(rootTransform);
    }

    public float Transition(bool toggle)
    {
        if (toggle)
        {
            return TransitionHandler.FadeIn();
        }
        
        return TransitionHandler.FadeOut();
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
        OnOpen();
    }

    protected virtual void OnOpen()
    {
        
    }

    public void Close()
    {
        gameObject.SetActive(false);
        OnClose();
    }

    protected virtual void OnClose()
    {
        
    }
}
