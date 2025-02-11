using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerSystem : SingletonGameSystem <UIControllerSystem>
{
    private IUIHudPanel activeController;
    public IUIHudPanel GameplayController;
    public IUIHudPanel MetaController;

    public bool HasActiveScreen => activeController != null;
    
    protected override void OnInitialize()
    { 
        base.OnInitialize();
    }
    
    public async UniTask PlayTransition(bool toggle)
    {
        await activeController.Transition(toggle);
    }
    
    public void OpenGameplay()
    {
        activeController?.Close();
        activeController = GameplayController;
        activeController.Open();
    }


    public void OpenMeta()
    {
        activeController?.Close();
        activeController = MetaController;
        activeController.Open();
    }
}