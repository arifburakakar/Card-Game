using System.Collections.Generic;
using UnityEngine;

public partial class Game
{
    private async void StartDealSequence()
    {
        Debug.Log("Deal Start");
        
        List<OID> hand =  player.Hand;
        for (int i = 0; i < hand.Count; i++)
        {
            BaseCard item = (BaseCard) GetCard(hand[i]);
            item.transform.SetParent(gameContainer.transform);
            item.SetPosition(boardProxy.DealerPoint.position);
            item.SetScale(Vector3.one);
            item.SetRotation(boardProxy.DealerPoint.eulerAngles);
            await item.PlayDealAnimation(CalculateCardTargetPosition(slotPoints[i]), gameplayManager.GameplayConfig);
            AddCardToSlot(item, i);
        }
        
        Debug.Log("Deal End");
    }

    private Vector3 CalculateCardTargetPosition(Vector3 targetPoint)
    {
        Vector3 position = Vector3.zero;
        if (targetPoint.x <= centerPoint.x)
        {
            position =  centerPoint;
        }
        else
        {
            position = targetPoint;
        }
        position += Vector3.up * gameplayManager.GameplayConfig.DealCardTargetOffsetHeight;
        return position;
    }
    
    private Item GetCard(OID oid)
    {
        return gameplayManager.PoolHandler.GetItem(oid);
    }
}