using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerSystem : SingletonGameSystem<GameControllerSystem>
{
    public bool IsMetaActive { get; private set; }

    private IManager metaManager;
    private IManager gameplayManager;


    public IEnumerator LoadMain()
    {
        bool hasActiveScreen = UIControllerSystem.Instance.HasActiveScreen;
        
        if (hasActiveScreen)
        {
            yield return UIControllerSystem.Instance.PlayTransition(true);
        }

        //todo repeat load gameplay fix it 
        if (metaManager == null)
        {
            yield return SceneManager.LoadSceneAsync(Main.Instance.MetaSceneName);
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
            yield return UIControllerSystem.Instance.PlayTransition(false);
        }
        
        metaManager.Execute();

        IsMetaActive = true;
    }

    public IEnumerator LoadGameplay()
    {
        yield return UIControllerSystem.Instance.PlayTransition(true);
        
        if (gameplayManager == null)
        {
            yield return SceneManager.LoadSceneAsync(Main.Instance.GameplaySceneName, LoadSceneMode.Additive);
            gameplayManager = new GameplayManager();
            gameplayManager.Initialize(SceneManager.GetSceneByName(Main.Instance.GameplaySceneName));
            gameplayManager.CreateManagerNecessary();
        }
        
        UIControllerSystem.Instance.OpenGameplay();
        metaManager.Disable();
        gameplayManager.Enable();
        
        yield return UIControllerSystem.Instance.PlayTransition(false);
        
        gameplayManager.Execute();
        
        IsMetaActive = false;
    }
}