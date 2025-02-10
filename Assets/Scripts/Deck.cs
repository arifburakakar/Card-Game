using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private Queue<OID> cards; 
    public Deck()
    {
        cards = CreateDeck();
        ShuffleDeck();
    }

    private Queue<OID> CreateDeck()
    {
        int symbolCount = 4;
        int perCardCount = 13;
        Queue<OID> cards = new Queue<OID>();

        for (int i = 1; i < symbolCount + 1; i++)
        {
            for (int j = 1; j < perCardCount + 1; j++)
            {
                OID card = new OID(i, j);
                cards.Enqueue(card);
            }
        }
        
        return cards;
    }

    private void ShuffleDeck()
    {
        cards.ShuffleQueue();
    }

    public OID GetCard()
    {
        return cards.Dequeue();
    }
    
    public void PrintDeck()
    {
        foreach (OID oid in cards)
        {
            Debug.Log($"ID: {oid.ObjectID} - VariantID: {oid.VariantID} - Value:{oid.GetValue()}");
        }
    }
}