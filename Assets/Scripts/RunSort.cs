using System.Collections.Generic;

public class RunSort : ISort
{
    public List<OID> Sort(List<OID> hand)
    {
        List<OID> sortedHand = new List<OID>();
        List<OID> deadwoodCards = new List<OID>();
        Dictionary<int, OID[]> groups = new Dictionary<int, OID[]>();

        for (int i = 0; i < hand.Count; i++)
        {
            OID oid = hand[i];
            if (groups.TryGetValue(oid.ObjectID, out OID[] group))
            {
                group[oid.VariantID] = oid;
            }
            else
            {
                OID[] newGroup = new OID[13];
                newGroup[oid.VariantID] = oid;
                groups.Add(oid.ObjectID, newGroup);
            }
        }

        foreach (int id in groups.Keys)
        {
            OID[] valueGroup = groups[id];

            List<OID> tempGroup = new List<OID>();
            for (int i = 0; i < valueGroup.Length; i++)
            {
                OID oid = valueGroup[i];
                if (oid == null)
                {
                    if (tempGroup.Count >= 3)
                    {
                        sortedHand.AddRange(tempGroup);
                        tempGroup.Clear();
                    }
                    {
                        deadwoodCards.AddRange(tempGroup);
                        tempGroup.Clear();
                    }
                    continue;
                }
                
                tempGroup.Add(oid);

                if (i == valueGroup.Length - 1)
                {
                    if (tempGroup.Count >= 3)
                    {
                        sortedHand.AddRange(tempGroup);
                        tempGroup.Clear();
                    }
                    {
                        deadwoodCards.AddRange(tempGroup);
                        tempGroup.Clear();
                    }
                }
            }
        }
        
        sortedHand.AddRange(deadwoodCards);
        
        return sortedHand;
    }
}