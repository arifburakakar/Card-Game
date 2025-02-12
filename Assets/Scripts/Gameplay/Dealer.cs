using System.Collections.Generic;

public class Dealer
{
    private Deck deck;
    private List<Player> players;
    private int PerPlayerCardCount = 11;
    
    public Dealer()
    {
        deck = new Deck();
        players = new List<Player>();
    }

    public void DealCards(bool isTest)
    {
        if (isTest)
        {
            List<OID> oids = new List<OID>();
            oids.Add(new OID(2, 1));
            oids.Add(new OID(1, 2));
            oids.Add(new OID(3, 5));
            oids.Add(new OID(2, 4));
            oids.Add(new OID(1, 1));
            oids.Add(new OID(3, 3));
            oids.Add(new OID(4, 4));
            oids.Add(new OID(1, 4));
            oids.Add(new OID(3, 1));
            oids.Add(new OID(1, 3));
            oids.Add(new OID(3, 4));
        
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < oids.Count; j++)
                {
                    players[i].AddCard(oids[j]);
                }
            }

        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < PerPlayerCardCount; j++)
                {
                    players[i].AddCard(deck.GetCard());
                }
            }
        }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }
}