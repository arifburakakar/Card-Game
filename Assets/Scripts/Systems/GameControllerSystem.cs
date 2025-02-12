using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerSystem : SingletonGameSystem<GameControllerSystem>
{
    public bool IsMetaActive { get; private set; }

    public IManager MetaManager;
    public IManager GameplayManager;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        
        PrimeTween.PrimeTweenConfig.warnZeroDuration = false;
        PrimeTween.PrimeTweenConfig.warnTweenOnDisabledTarget = false;
        PrimeTween.PrimeTweenConfig.warnEndValueEqualsCurrent = false;
    }

    public async void LoadMeta()
    {
        bool hasActiveScreen = UIControllerSystem.Instance.HasActiveScreen;
        
        if (hasActiveScreen)
        {
            await UIControllerSystem.Instance.PlayTransition(true);
        }

        //todo repeat load gameplay fix it 
        if (MetaManager == null)
        {
            await SceneManager.LoadSceneAsync(Main.Instance.MetaSceneName);
            MetaManager = new MetaManager();
            MetaManager.Initialize(SceneManager.GetSceneByName(Main.Instance.MetaSceneName));
            MetaManager.CreateManagerNecessary();
        }

        UIControllerSystem.Instance.OpenMeta();
        MetaManager.Enable();
        GameplayManager?.Disable();
        GameUtility.GCCollectDefault();
        
        if (hasActiveScreen)
        {
            await UIControllerSystem.Instance.PlayTransition(false);
        }
        
        MetaManager.Execute();

        IsMetaActive = true;
    }

    public async void LoadGameplay()
    {
        await UIControllerSystem.Instance.PlayTransition(true);
        
        if (GameplayManager == null)
        {
            await SceneManager.LoadSceneAsync(Main.Instance.GameplaySceneName, LoadSceneMode.Additive);
            GameplayManager = new GameplayManager();
            GameplayManager.Initialize(SceneManager.GetSceneByName(Main.Instance.GameplaySceneName));
            GameplayManager.CreateManagerNecessary();
        }
        
        UIControllerSystem.Instance.OpenGameplay();
        MetaManager.Disable();
        GameplayManager.Enable();
        
        await UIControllerSystem.Instance.PlayTransition(false);
        
        GameplayManager.Execute();
        
        IsMetaActive = false;
    }
}