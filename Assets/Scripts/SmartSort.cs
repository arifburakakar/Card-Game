using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmartSort : ISort
{
    public List<OID> Sort(List<OID> hand)
    {
        List<List<OID>> setGroups = FindAllPossibleSets(hand);
        List<List<OID>> runGroups = FindAllPossibleRuns(hand);
        
        List<List<List<OID>>> allCombinations = GetCombinations(setGroups, runGroups);

        int bestCombinationIndex = 0;
        int maxValue = 0;
        for (int i = 0; i < allCombinations.Count; i++)
        {
            List<List<OID>> combination = allCombinations[i];
            int currentValue = 0;
            for (int j = 0; j < combination.Count; j++)
            {
                List<OID> group = combination[j];
                for (int k = 0; k < group.Count; k++)
                {
                    OID oid = group[k];
                    currentValue += oid.GetValue();
                }
            }

            if (currentValue > maxValue)
            {
                maxValue = currentValue;
                bestCombinationIndex = i;
            }
        }

        List<OID> sortedHand = new List<OID>();
        List<OID> deadwood = new List<OID>(hand);
        List<List<OID>> bestCombination = allCombinations[bestCombinationIndex];
        for (int i = 0; i < bestCombination.Count; i++)
        {
            List<OID> group = bestCombination[i];
            for (int j = 0; j < group.Count; j++)
            {
                OID oid = group[j];
                sortedHand.Add(oid);
                deadwood.Remove(oid);
            }
        }
         
        sortedHand.AddRange(deadwood);
        return sortedHand;
    }

    private List<List<OID>> FindAllPossibleSets(List<OID> hand)
    {
        List<List<OID>> sortedHand = new List<List<OID>>();
        List<int> ids = new List<int>();
        
        for (int i = 0; i < hand.Count; i++)
        {
            OID oid = hand[i];
            
            if (ids.Contains(oid.VariantID))
            {
                continue;
            }

            List<OID> oids = new List<OID>();
            int groupCount = 0;
             
            for (int j = i + 1; j < hand.Count; j++)
            {
                OID nextOID = hand[j];
                if (nextOID.VariantID == oid.VariantID)
                {
                    oids.Add(nextOID);
                    groupCount++;
                }
            }

            if (groupCount >= 2)
            {
                oids.Add(oid);
                ids.Add(oid.VariantID);

                if (oids.Count == 4)
                {
                    List<List<OID>> subsets = new List<List<OID>>();
                    for (int x = 0; x < oids.Count; x++)
                    {
                        List<OID> combination = new List<OID>();
                        for (int j = 0; j < oids.Count; j++)
                        {
                            if (x != j) combination.Add(oids[j]);
                        }
                        subsets.Add(combination);
                    }
                    sortedHand.AddRange(subsets);
                }
                
                sortedHand.Add(oids);
            }
        }
        
        return sortedHand;
    }
    
    private List<List<OID>> FindAllPossibleRuns(List<OID> hand)
    {
        List<List<OID>> sortedHand = new List<List<OID>>();
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
                        sortedHand.Add(new List<OID>(tempGroup));
                    }
                    
                    tempGroup.Clear();
                    continue;
                }
                
                tempGroup.Add(oid);

                if (i == valueGroup.Length - 1)
                {
                    if (tempGroup.Count >= 3)
                    {
                        sortedHand.Add(new List<OID>(tempGroup));
                    }
                    tempGroup.Clear();
                }
            }
        }

        List<List<OID>> subs = new List<List<OID>>();
        for (int i = 0; i < sortedHand.Count; i++)
        {
            AddSubsets(sortedHand[i], subs);
        }
        
        return subs;
    }
    
    private void AddSubsets(List<OID> tempGroup, List<List<OID>> sortedHand)
    {
        int n = tempGroup.Count;
        
        for (int size = 3; size <= n; size++)
        {
            for (int i = 0; i <= n - size; i++)
            {
                List<OID> subset = new List<OID>();
                
                for (int j = i; j < i + size; j++)
                {
                    subset.Add(tempGroup[j]);
                }
                sortedHand.Add(subset);
            }
        }
    }

    private List<List<List<OID>>> GetCombinations(List<List<OID>> sets, List<List<OID>> runs)
    {
        List<List<OID>> allGroups = new List<List<OID>>();
        allGroups.AddRange(sets);
        allGroups.AddRange(runs);
        
        List<List<List<OID>>> combinations = new List<List<List<OID>>>();
        GenerateCombinations(allGroups, new List<List<OID>>(), 0, combinations);
        return combinations;
    }

    private void GenerateCombinations(List<List<OID>> allGroups, List<List<OID>> currentCombination, int startIndex, List<List<List<OID>>> combinations)
    {
        if (currentCombination.Count > 0)
        {
            combinations.Add(new List<List<OID>>(currentCombination));
        }
        
        for (int i = startIndex; i < allGroups.Count; i++)
        {
            List<OID> group = allGroups[i];
            
            if (!IsOverlapping(currentCombination, group))
            {
                currentCombination.Add(group);
                GenerateCombinations(allGroups, currentCombination, i + 1, combinations);
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
    }

    private bool IsOverlapping(List<List<OID>> combination, List<OID> group)
    {
        for (var i = 0; i < combination.Count; i++)
        {
            var existingGroup = combination[i];
            if (GroupsOverlap(existingGroup, group))
                return true;
        }

        return false;
    }

    private bool GroupsOverlap(List<OID> group1, List<OID> group2)
    {
        for (var i = 0; i < group1.Count; i++)
        {
            var oid = group1[i];
            if (group2.Contains(oid))
                return true;
        }

        return false;
    }
}