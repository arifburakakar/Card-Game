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
       (List<List<OID>> groups, List<OID> deadwood) sortResult = sort.Sort(hand);
       
       List<OID> temp = new List<OID>();
       foreach (List<OID> groups in sortResult.groups)
       {
           temp.AddRange(groups);
       }
       temp.AddRange(sortResult.deadwood);

       hand = temp;
    }

    public void PrintHand()
    {
        foreach (OID oid in hand)
        {
            Debug.Log($"ID: {oid.ObjectID} - VariantID: {oid.VariantID} - Value:{oid.GetValue()}");
        }
    }
}