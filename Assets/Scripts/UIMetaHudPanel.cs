public class UIMetaHudPanel : UIBaseHudPanel
{
    public void OnClickPlay()
    {
        CoroutineHandler.Instance.Begin(GameControllerSystem.Instance.LoadGameplay());
    }
}
