using System.Collections.Generic;

public class SetSort : ISort
{
    public List<OID> Sort(List<OID> hand)
    {
        List<OID> sortedHand = new List<OID>();
        List<OID> deadwoodCards = new List<OID>();
        
        for (int i = 0; i < hand.Count; i++)
        {
            OID oid = hand[i];
            
            if (sortedHand.Contains(oid))
            {
                continue;
            }

            int groupCount = 0;
             
            for (int j = i + 1; j < hand.Count; j++)
            {
                OID nextOID = hand[j];
                if (nextOID.VariantID == oid.VariantID)
                {
                    sortedHand.Add(nextOID);
                    groupCount++;
                }
            }

            if (groupCount >= 2)
            {
                sortedHand.Add(oid);
            }
            else
            {
                deadwoodCards.Add(oid);
            }
        }
        
        sortedHand.AddRange(deadwoodCards);

        return sortedHand;
    }
}