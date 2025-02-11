using UnityEngine;

public partial class Game
{
    private void InitializeInput()
    {
        Main.Instance.TouchPressed += OnTouchPressed;
        Main.Instance.TouchHold += OnTouchHold;
        Main.Instance.TouchRelease += OnTouchRelease;
    }
    
    private void ResetInput()
    {
        Main.Instance.TouchPressed -= OnTouchPressed;
        Main.Instance.TouchHold -= OnTouchHold;
        Main.Instance.TouchRelease -= OnTouchRelease;
    }

    private void OnTouchPressed(Vector2 inputPosition)
    {
        TrySelectCard(inputPosition); 
    }

    private void OnTouchHold(Vector2 inputPosition)
    {
        TryUpdateSelectedCard(inputPosition);
    }

    private void OnTouchRelease(Vector2 inputPosition)
    {
        TryDeselectCard();
    }
}