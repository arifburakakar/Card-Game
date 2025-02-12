using System.Collections.Generic;
using Unity.VisualScripting;

public class SetSort : BaseSort
{
    public override (List<List<OID>>,List<OID>) Sort(List<OID> hand)
    {
        List<List<OID>> groups = new List<List<OID>>();
        List<int> ids = new List<int>();
        
        for (int i = 0; i < hand.Count; i++)
        {
            OID oid = hand[i];
            
            if (ids.Contains(oid.VariantID))
            {
                continue;
            }
            
            List<OID> oids = new List<OID>();
            oids.Add(oid);
            int groupCount = 1;
             
            for (int j = i + 1; j < hand.Count; j++)
            {
                OID nextOID = hand[j];
                if (nextOID.VariantID == oid.VariantID)
                {
                    oids.Add(nextOID);
                    groupCount++;
                }
            }

            if (groupCount >= 3)
            {
                ids.Add(oid.VariantID);
                groups.Add(oids);
            }
        }
        
        var deadwood = Deadwood(hand, groups);
        
        return (groups, deadwood);
    }
}