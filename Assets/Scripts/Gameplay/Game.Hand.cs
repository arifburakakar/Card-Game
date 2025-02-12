using System.Collections.Generic;
using UnityEngine;

public partial class Game
{
    private Item selectedItem;
    private Vector3[] slotPoints;
    private Item[] cards;
    private Vector3 centerPoint;
    private float deltaWidth;
    private Item currentLeftNeighbor;
    private Item currentRightNeighbor;
    private bool neighborsActive;
    private Vector3 leftSide;
    private Dictionary<Item, float> currentTMap = new Dictionary<Item, float>();
    private Dictionary<Item, float> targetTMap = new Dictionary<Item, float>();


    private void CreateSlots()
    {
        slotPoints = new Vector3[gameplayManager.GameplayConfig.DefaultHandCount];
        cards = new Item[slotPoints.Length];
        centerPoint = new Vector3(boardProxy.HandPoint.transform.position.x,boardProxy.HandPoint.transform.position.y + gameplayManager.GameplayConfig.HandYPosition);
        deltaWidth = gameplayManager.GameplayConfig.HandWidth / slotPoints.Length;
        leftSide = centerPoint + Vector3.left * deltaWidth * (slotPoints.Length / 2);

        for (int i = 0; i < slotPoints.Length; i++)
        {
            float t = (float)i / (slotPoints.Length - 1);
            slotPoints[i] = GetTargetCurvePosition(t);
        }
    }

    private async void AddCardToSlot(BaseCard item, int index)
    {
        await item.PlayOpenAnimation();
        cards[index] = item;
        float t = (float)index / (slotPoints.Length - 1);
        currentTMap[item] = t;
        targetTMap[item] = t;
    }
    
    private void UpdatePositions()
    {
        for (int i = 0; i < slotPoints.Length; i++)
        {
            Item card = cards[i];
            
            float t = (float)i / (slotPoints.Length - 1);
            // for runtime width change
            slotPoints[i] = GetTargetCurvePosition(t);
            
            if (card == null || card == selectedItem)
                continue;
            
            targetTMap[card] = t;
            
            float currentT = currentTMap.ContainsKey(card) ? FindClosestTOnCurve(card.transform.position) : t;
            currentT = Mathf.MoveTowards(currentT, t,
                Time.deltaTime * gameplayManager.GameplayConfig.CardSlotMovementSpeed);
            currentTMap[card] = currentT;

            Vector3 targetPosition = GetTargetCurvePosition(currentT);
            card.SetPosition(Vector3.MoveTowards(card.transform.position, targetPosition, Time.deltaTime * gameplayManager.GameplayConfig.CardSlotMovementSpeed));
            card.SetRotation(Vector3.forward * CalculateZRotation(targetPosition));
        }
    }
    
    private Vector3 GetTargetCurvePosition(float t)
    {
        int slotCount = slotPoints.Length;

        if (slotCount == 0)
            return centerPoint;

        if (slotCount == 1)
            return centerPoint;

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
        float u = 1 - t;
        float uu = Mathf.Pow(u, 2);
        float uuu = Mathf.Pow(u, 3);
        float tt = Mathf.Pow(t, 2);
        float ttt = Mathf.Pow(t, 3);

        Vector3 point = uuu * p0;
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2; 
        point += ttt * p3;

        return point;
    }
    
    private void TrySelectCard(Vector2 inputPosition)
    {
        // find better way without raycast and collider, pointerclick, position check?

        RaycastHit2D[] hits = Physics2D.RaycastAll(inputPosition, Vector2.zero, 10);
        if (hits.Length > 0)
        {
            Item frontItem = null;
            int targetIndex = -1;
            for (int i = 0; i < hits.Length; i++)
            {
                Item item = hits[i].transform.GetComponent<Item>();
                if (item)
                {
                    int index = GetItemIndex(item);
                    if (index > targetIndex)
                    {
                        targetIndex = index;
                        frontItem = item;
                    }
                }
            }
            

            
            if (frontItem)
            {
                selectedItem = frontItem;
                selectedItem.Select();
            }
        }
    }
    
    private int GetSelectedItemIndex()
    {
        return GetItemIndex(selectedItem);
    }

    private int GetItemIndex(Item item)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (item == cards[i])
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
        
        float closestT = FindClosestTOnCurve(inputPosition);
        currentTMap[selectedItem] = closestT;
        
        Vector3 targetPosition = new Vector3(isDrag ? inputPosition.x : slotPoints[GetSelectedItemIndex()].x, centerPoint.y + gameplayManager.GameplayConfig.SelectHeightOffset);
        selectedItem.SetPosition(Vector3.Lerp(selectedItem.transform.position, targetPosition, Time.deltaTime * gameplayManager.GameplayConfig.DragSpeed));
        selectedItem.SetRotation(Vector3.forward * CalculateZRotation(selectedItem.transform.position));

        int currentIndex = GetSelectedItemIndex();
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
    
    private float FindClosestTOnCurve(Vector2 inputPosition)
    {
        float closestT = 0;
        float closestDistance = float.MaxValue;

        for (float t = 0; t <= 1; t += 0.01f)
        {
            Vector3 curvePoint = GetTargetCurvePosition(t);
            float distance = Vector2.Distance(inputPosition, curvePoint);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestT = t;
            }
        }

        return closestT;
    }
    
    private void ResetCurrentNeighbors()
    {
        if (currentLeftNeighbor != null)
        {
            currentLeftNeighbor.ScaleAnimation(1, .1f);
            currentLeftNeighbor = null;
        }
        
        if (currentRightNeighbor != null)
        {
            currentRightNeighbor.ScaleAnimation(1, .1f);
            currentRightNeighbor = null;
        }
        
        neighborsActive = false;
    }

    private void ActivateNewNeighbors()
    {
        if (selectedItem == null) return;

        neighborsActive = true;
        
        if (currentLeftNeighbor != null)
            currentLeftNeighbor.ScaleAnimation(1.15f, .1f);
        
        if (currentRightNeighbor != null)
            currentRightNeighbor.ScaleAnimation(1.15f, .1f);
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

        int currentIndex = GetSelectedItemIndex();
        float targetT = (float)currentIndex / (slotPoints.Length - 1);
        targetTMap[selectedItem] = targetT;
        ResetCurrentNeighbors();
        selectedItem.Deselect();
        selectedItem = null;
    }

}