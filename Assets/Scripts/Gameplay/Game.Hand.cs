using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public partial class Game
{
    private Item selectedItem;
    private Vector3[] slotPoints;
    private Item[] cards; // may be linklist
    private Vector3 centerPoint;

    private void CreateSlots()
    {
        slotPoints = new Vector3[gameplayManager.GameplayConfig.DefaultHandCount];
        cards = new Item[slotPoints.Length];
        centerPoint = new Vector3(boardProxy.HandPoint.transform.position.x,boardProxy.HandPoint.transform.position.y + gameplayManager.GameplayConfig.HandYPosition);
        float deltaWidth = gameplayManager.GameplayConfig.HandWidth / slotPoints.Length;
        Vector3 leftSide = centerPoint + Vector3.left * deltaWidth * (slotPoints.Length / 2);

        for (int i = 0; i < slotPoints.Length; i++)
        {
            Vector3 slotPosition = leftSide + Vector3.right * deltaWidth * i;
            slotPoints[i] = slotPosition;
        }
    }

    private async void AddCardToSlot(BaseCard item, int index)
    {
        await item.PlayOpenAnimation(gameplayManager.GameplayConfig);
        cards[index] = item;
    }
    
    private void UpdateHandCardPositions()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            // temp
            Item card = cards[i];
            if (card == null)
            {
                continue;
            }
            card.SetPosition(Vector3.MoveTowards(card.transform.position,  slotPoints[i], Time.deltaTime * gameplayManager.GameplayConfig.CardSlotMovementSpeed)); 
        }
    }

    private Vector3 GetTargetCurvePosition()
    {
        return Vector3.zero;
    }

    private void SelectCard()
    {
        
    }
}