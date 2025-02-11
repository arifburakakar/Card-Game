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
            Item card = cards[i];
            if (card == null)
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

    private Item SelectCard()
    {
        return null;
    }
}