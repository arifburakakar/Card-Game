public class UIMetaHudPanel : UIBaseHudPanel
{
    public void OnClickPlay()
    {
        GameControllerSystem.Instance.LoadGameplay();
    }
}
