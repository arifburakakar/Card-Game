public class UIMetaHudPanel : UIBaseHudPanel
{
    public void OnClickPlay()
    {
        GameControllerSystem.Instance.LoadGameplay();
    }

    public void OnClickUseZyngaCards()
    {
        GameControllerSystem.Instance.UseZyngaCaseCards = true;
        GameControllerSystem.Instance.LoadGameplay();
    }

    public void OnClickUseDefaultCards()
    {
        GameControllerSystem.Instance.UseZyngaCaseCards = false;
        GameControllerSystem.Instance.LoadGameplay();
    }
}
