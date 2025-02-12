using System.Collections.Generic;

public class RunSort : BaseSort
{
    public override (List<List<OID>>,List<OID>) Sort(List<OID> hand)
    {
        List<List<OID>> groups = new List<List<OID>>();
        Dictionary<int, OID[]> oidMap = new Dictionary<int, OID[]>();

        for (int i = 0; i < hand.Count; i++)
        {
            OID oid = hand[i];
            if (oidMap.TryGetValue(oid.ObjectID, out OID[] group))
            {
                group[oid.VariantID] = oid;
            }
            else
            {
                OID[] newGroup = new OID[13];
                newGroup[oid.VariantID] = oid;
                oidMap.Add(oid.ObjectID, newGroup);
            }
        }

        foreach (int id in oidMap.Keys)
        {
            OID[] valueGroup = oidMap[id];

            List<OID> tempGroup = new List<OID>();
            for (int i = 0; i < valueGroup.Length; i++)
            {
                OID oid = valueGroup[i];
                if (oid == null)
                {
                    if (tempGroup.Count >= 3)
                    {
                        groups.Add(new List<OID>(tempGroup));
                    }
                    
                    tempGroup.Clear();
                    continue;
                }
                
                tempGroup.Add(oid);

                if (i == valueGroup.Length - 1)
                {
                    if (tempGroup.Count >= 3)
                    {
                        groups.Add(new List<OID>(tempGroup));
                        tempGroup.Clear();
                    }
                    
                    tempGroup.Clear();
                }
            }
        }
        
        List<OID> deadwood = Deadwood(hand, groups);
        return (groups, deadwood);
    }
}