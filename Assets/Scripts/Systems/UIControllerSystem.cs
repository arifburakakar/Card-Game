using System.Collections;
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
    
    public IEnumerator PlayTransition(bool toggle)
    {
        float duration = activeController.Transition(toggle);
        yield return new WaitForSeconds(duration);
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