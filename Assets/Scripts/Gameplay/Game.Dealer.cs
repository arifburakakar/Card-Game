using System.Collections.Generic;
using UnityEngine;

public partial class Game
{
    private async void StartDealSequence()
    {
        Debug.Log("Deal End");
        
        List<OID> hand =  player.Hand;
        for (int i = 0; i < hand.Count; i++)
        {
            BaseCard item = (BaseCard) GetCard(hand[i]);
            item.transform.SetParent(gameContainer.transform);
            item.SetPosition(boardProxy.DealerPoint.position);
            item.SetScale(Vector3.one);
            item.SetRotation(boardProxy.DealerPoint.eulerAngles);
            await item.PlayDealAnimation(slotPoints[i], gameplayManager.GameplayConfig);
        }
        
        Debug.Log("Deal End");
    }
    
    private Item GetCard(OID oid)
    {
        return gameplayManager.PoolHandler.GetItem(oid);
    }
}