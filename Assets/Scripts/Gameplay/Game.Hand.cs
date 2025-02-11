using PrimeTween;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public partial class Game
{
    private Item selectedItem;
    private Vector3[] slotPoints;
    private Item[] cards; // may be linklist for swap 
    private Vector3 centerPoint;
    private float deltaWidth;
    private Item currentLeftNeighbor;
    private Item currentRightNeighbor;
    private bool neighborsActive;

    private void CreateSlots()
    {
        slotPoints = new Vector3[gameplayManager.GameplayConfig.DefaultHandCount];
        cards = new Item[slotPoints.Length];
        centerPoint = new Vector3(boardProxy.HandPoint.transform.position.x,boardProxy.HandPoint.transform.position.y + gameplayManager.GameplayConfig.HandYPosition);
        deltaWidth = gameplayManager.GameplayConfig.HandWidth / slotPoints.Length;
        Vector3 leftSide = centerPoint + Vector3.left * deltaWidth * (slotPoints.Length / 2);

        for (int i = 0; i < slotPoints.Length; i++)
        {
            Vector3 slotPosition = leftSide + Vector3.right * deltaWidth * i;
            slotPoints[i] = slotPosition;
        }
    }

    private async void AddCardToSlot(BaseCard item, int index)
    {
        await item.PlayOpenAnimation();
        cards[index] = item;
    }
    
    private void UpdateHandCardPositions()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            Item card = cards[i];
            if (card == null || card == selectedItem)
                continue;
            
            card.SetPosition(Vector3.MoveTowards(card.transform.position, GetTargetCurvePosition(i), Time.deltaTime * gameplayManager.GameplayConfig.CardSlotMovementSpeed));
            card.SetRotation(Vector3.forward * CalculateZRotation(card.transform.position));
        }
    }
    
    private Vector3 GetTargetCurvePosition(int index)
    {
        int slotCount = slotPoints.Length;

        if (slotCount == 0)
            return centerPoint;

        if (slotCount == 1)
            return centerPoint;

        float t = (float)index / (slotCount - 1);

        float handWidth = gameplayManager.GameplayConfig.HandWidth;
        Vector3 startPoint = centerPoint + Vector3.left * handWidth * 0.5f;
        Vector3 endPoint = centerPoint + Vector3.right * handWidth * 0.5f;

        float theta = gameplayManager.GameplayConfig.HandArcAngle * Mathf.Deg2Rad;
        float radius = handWidth / (2 * Mathf.Sin(theta * 0.5f));
        float curveHeight = radius * (1 - Mathf.Cos(theta * 0.5f));

        Vector3 controlPoint1 = startPoint + new Vector3(handWidth * 0.25f, curveHeight, 0);
        Vector3 controlPoint2 = endPoint - new Vector3(handWidth * 0.25f, -curveHeight, 0);

        return CalculateCubicBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
    }
    
    private float CalculateZRotation(Vector3 cardPosition)
    {
        float horizontalDistance = (cardPosition.x - centerPoint.x) / (gameplayManager.GameplayConfig.HandWidth * 0.5f);
        float rotationZ = Mathf.Sin(horizontalDistance * (Mathf.PI * 0.5f)) * -gameplayManager.GameplayConfig.RotationMultiplier;
        return rotationZ;
    }

    private Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        
        // fix here
        float u = 1 - t;
        float uu = u * u;
        float uuu = uu * u;
        float tt = t * t;
        float ttt = tt * t;

        Vector3 point = uuu * p0;
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2; 
        point += ttt * p3;

        return point;
    }
    
    private void TrySelectCard(Vector2 inputPosition)
    {
        // find better way without raycast and collider, pointerclick, position check?

        RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero, 10);
        if (hit)
        {
            Item item = hit.transform.GetComponent<Item>();
            if (item)
            {
                selectedItem = item;
                selectedItem.Select();
            }
        }
    }
    
    private int GetItemIndex()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (selectedItem == cards[i])
            {
                return i;
            }
        }

        return -1; // exception
    }

    private void TryUpdateSelectedCard(Vector2 inputPosition)
    {
        if (!selectedItem)
        {
            return;
        }

        Vector3 targetPosition = new Vector3(inputPosition.x, centerPoint.y + gameplayManager.GameplayConfig.SelectHeightOffset);
        selectedItem.SetPosition(Vector3.Lerp(selectedItem.transform.position, targetPosition, Time.deltaTime * gameplayManager.GameplayConfig.DragSpeed));
        selectedItem.SetRotation(Vector3.forward * CalculateZRotation(selectedItem.transform.position));

        int currentIndex = GetItemIndex();
        Item newLeft = currentIndex > 0 ? cards[currentIndex - 1] : null;
        Item newRight = currentIndex < cards.Length - 1 ? cards[currentIndex + 1] : null;
        if (currentLeftNeighbor != newLeft || currentRightNeighbor != newRight)
        {
            ResetCurrentNeighbors();
            currentLeftNeighbor = newLeft;
            currentRightNeighbor = newRight;
            ActivateNewNeighbors();
        }
        
        if (currentIndex < slotPoints.Length - 1)
        {
            float midpointRight = (slotPoints[currentIndex].x + slotPoints[currentIndex + 1].x) / 2f;
            if (selectedItem.transform.position.x > midpointRight)
            {
                ChangeItems(currentIndex, currentIndex + 1);
            }
        }
        
        if (currentIndex > 0)
        {
            float midpointLeft = (slotPoints[currentIndex - 1].x + slotPoints[currentIndex].x) / 2f;
            if (selectedItem.transform.position.x < midpointLeft)
            {
                ChangeItems(currentIndex - 1, currentIndex);
            }
        }
    }
    
    private void ResetCurrentNeighbors()
    {
        if (currentLeftNeighbor != null)
        {
            currentLeftNeighbor.SetScale(Vector3.one);;
            currentLeftNeighbor = null;
        }
        
        if (currentRightNeighbor != null)
        {
            currentRightNeighbor.SetScale(Vector3.one);;
            currentRightNeighbor = null;
        }
        
        neighborsActive = false;
    }

    private void ActivateNewNeighbors()
    {
        if (selectedItem == null) return;

        neighborsActive = true;
        
        if (currentLeftNeighbor != null)
            currentLeftNeighbor.SetScale( Vector3.one * 1.25f);;
        
        if (currentRightNeighbor != null)
            currentRightNeighbor.SetScale( Vector3.one * 1.25f);;
    }

    private void ChangeItems(int indexA, int indexB)
    {
        (cards[indexA], cards[indexB]) = (cards[indexB], cards[indexA]);
    }

    private void TryDeselectCard()
    {
        if (!selectedItem)
        {
            return;
        }
        
        ResetCurrentNeighbors();
        selectedItem.Deselect();
        selectedItem = null;
    }
}