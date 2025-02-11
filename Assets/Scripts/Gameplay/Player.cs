using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private List<OID> hand;

    public List<OID> Hand => hand;
    
    public Player()
    {
        hand = new List<OID>();
    }

    public void AddCard(OID card)
    {
        hand.Add(card);
    }

    public void RemoveCard(OID card)
    {
        hand.Remove(card);
    }

    public void SortHand(ISort sort)
    {
        hand = sort.Sort(hand);
    }

    public void PrintHand()
    {
        foreach (OID oid in hand)
        {
            Debug.Log($"ID: {oid.ObjectID} - VariantID: {oid.VariantID} - Value:{oid.GetValue()}");
        }
    }
}