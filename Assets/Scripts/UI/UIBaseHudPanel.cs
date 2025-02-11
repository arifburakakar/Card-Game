using System;
using Cysharp.Threading.Tasks;
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

    public async UniTask Transition(bool toggle)
    {
        if (toggle)
        {
            await TransitionHandler.FadeIn();
        }
        else
        {
            await TransitionHandler.FadeOut();
        }
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
