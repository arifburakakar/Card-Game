using UnityEngine;

public partial class Game
{
    private bool isDrag;

    private float initialX = 0;
    private float dragOffset = .65f;
    
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
        isDrag = false;
        TrySelectCard(inputPosition);
        initialX = inputPosition.x;
    }

    private void OnTouchHold(Vector2 inputPosition)
    {
        if (!isDrag && dragOffset < Mathf.Abs(initialX - inputPosition.x))
        {
            isDrag = true;
        }
        TryUpdateSelectedCard(inputPosition);
    }

    private void OnTouchRelease(Vector2 inputPosition)
    {
        TryDeselectCard();
        isDrag = false;
    }
}