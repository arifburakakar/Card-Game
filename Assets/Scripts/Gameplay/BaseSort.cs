using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSort : ISort
{
    public abstract (List<List<OID>>,List<OID>) Sort(List<OID> hand);
    
    protected List<OID> Deadwood(List<OID> hand, List<List<OID>> groups)
    {
        List<OID> deadwood = new List<OID>(hand);

        if (groups == null || groups.Count == 0)
        {
            Debug.Log("Possible group count is 0");
            return deadwood;
        }
        
        for (int i = 0; i < groups.Count; i++)
        {
            List<OID> group = groups[i];
            for (int j = 0; j < group.Count; j++)
            {
                OID oid = group[j];
                deadwood.Remove(oid);
            }
        }

        return deadwood;
    }
}