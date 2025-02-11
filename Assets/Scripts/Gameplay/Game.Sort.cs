using System.Collections.Generic;

public partial class Game
{
    public void SetSort()
    {
        Sort(new SetSort());
    }

    public void RunSort()
    {
        Sort(new RunSort());
    }

    public void SmartSort()
    {
        Sort(new SmartSort());
    }


    private void Sort(ISort sort)
    {
        player.SortHand(sort);
        SortVisualCardList(player.Hand);
    }
    
    // better way n2
    private void SortVisualCardList(List<OID> oids)
    {
        var sortedCards = new Item[cards.Length];
        for (int i = 0; i < oids.Count; i++)
        {
            var oid = oids[i];
            for (int j = 0; j < cards.Length; j++)
            {
                var card = cards[j];
                if (card.OID.Equals(oid))
                {
                    sortedCards[i] = card;
                }
            }
        }

        cards = sortedCards;
    }

}