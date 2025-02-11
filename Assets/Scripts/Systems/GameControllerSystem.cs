using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerSystem : SingletonGameSystem<GameControllerSystem>
{
    public bool IsMetaActive { get; private set; }

    private IManager metaManager;
    private IManager gameplayManager;


    public async void LoadMain()
    {
        bool hasActiveScreen = UIControllerSystem.Instance.HasActiveScreen;
        
        if (hasActiveScreen)
        {
            await UIControllerSystem.Instance.PlayTransition(true);
        }

        //todo repeat load gameplay fix it 
        if (metaManager == null)
        {
            await SceneManager.LoadSceneAsync(Main.Instance.MetaSceneName);
            metaManager = new MetaManager();
            metaManager.Initialize(SceneManager.GetSceneByName(Main.Instance.MetaSceneName));
            metaManager.CreateManagerNecessary();
        }

        UIControllerSystem.Instance.OpenMeta();
        metaManager.Enable();
        gameplayManager?.Disable();
        GameUtility.GCCollectDefault();
        
        if (hasActiveScreen)
        {
            await UIControllerSystem.Instance.PlayTransition(false);
        }
        
        metaManager.Execute();

        IsMetaActive = true;
    }

    public async void LoadGameplay()
    {
        await UIControllerSystem.Instance.PlayTransition(true);
        
        if (gameplayManager == null)
        {
            await SceneManager.LoadSceneAsync(Main.Instance.GameplaySceneName, LoadSceneMode.Additive);
            gameplayManager = new GameplayManager();
            gameplayManager.Initialize(SceneManager.GetSceneByName(Main.Instance.GameplaySceneName));
            gameplayManager.CreateManagerNecessary();
        }
        
        UIControllerSystem.Instance.OpenGameplay();
        metaManager.Disable();
        gameplayManager.Enable();
        
        await UIControllerSystem.Instance.PlayTransition(false);
        
        gameplayManager.Execute();
        
        IsMetaActive = false;
    }
}