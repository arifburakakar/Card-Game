using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class SortTests
{
    private ISort setSort;
    private ISort runSort;
    private ISort smartSort;

    [SetUp]
    public void Setup()
    {
        setSort = new SetSort();
        runSort = new RunSort();
        smartSort = new SmartSort();
    }

    [Test]
    public void SetSort_ShouldSortCorrectly()
    {
        List<OID> hand = new List<OID>();
        List<List<OID>> sortGroups = new List<List<OID>>();
        List<OID> group1 = new List<OID>();
        List<OID> group2 = new List<OID>();
        List<OID> deadwood = new List<OID>();
        
        hand.Add(new OID(2, 1));
        hand.Add(new OID(1, 2));
        hand.Add(new OID(3, 5));
        hand.Add(new OID(2, 4));
        hand.Add(new OID(1, 1));
        hand.Add(new OID(3, 3));
        hand.Add(new OID(4, 4));
        hand.Add(new OID(1, 4));
        hand.Add(new OID(3, 1));
        hand.Add(new OID(1, 3));
        hand.Add(new OID(3, 4));
        
        group1.Add(new OID(1, 1));
        group1.Add(new OID(2, 1));
        group1.Add(new OID(3, 1));
        
        group2.Add(new OID(1, 4));
        group2.Add(new OID(2, 4));
        group2.Add(new OID(3, 4));
        group2.Add(new OID(4, 4));
        
        sortGroups.Add(group1);
        sortGroups.Add(group2);
        
        deadwood.Add(new OID(1, 2));
        deadwood.Add(new OID(3, 3));
        deadwood.Add(new OID(3, 5));
        deadwood.Add(new OID(1, 3));

        bool allGroupsLegit = true;
        
        (List<List<OID>> groups, List<OID> deadwood) sortedHand = setSort.Sort(hand);
        
        Assert.AreEqual(sortGroups.Count, sortedHand.groups.Count, "Group count is not equal");

        foreach (var expectedGroup in sortGroups)
        {
            bool groupFound = false;

            foreach (var actualGroup in sortedHand.groups)
            {
                if (actualGroup.Count == expectedGroup.Count)
                {
                    bool allOidsMatch = expectedGroup.All(expectedOid =>
                        actualGroup.Any(actualOid => actualOid.Equals(expectedOid))
                    );

                    if (allOidsMatch)
                    {
                        groupFound = true;
                        break;
                    }
                }
            }

            Assert.IsTrue(
                groupFound,
                $"Target group [{string.Join(", ", expectedGroup.Select(o => $"({o.ObjectID},{o.VariantID})"))}] is not found."
            );
        }

        Assert.AreEqual(
            deadwood.Count,
            sortedHand.deadwood.Count,
            "Deadwood count is not equal."
        );

        foreach (var expectedOid in deadwood)
        {
            bool found = sortedHand.deadwood.Any(actualOid => actualOid.Equals(expectedOid));
            Assert.IsTrue(
                found,
                $"Target deadwood item ({expectedOid.ObjectID},{expectedOid.VariantID}) is not found."
            );
        }
    }

    [Test]
    public void RunSort_ShouldSortCorrectly()
    {
        List<OID> hand = new List<OID>();
        List<List<OID>> sortGroups = new List<List<OID>>();
        List<OID> group1 = new List<OID>();
        List<OID> group2 = new List<OID>();
        List<OID> deadwood = new List<OID>();
        
        hand.Add(new OID(2, 1));
        hand.Add(new OID(1, 2));
        hand.Add(new OID(3, 5));
        hand.Add(new OID(2, 4));
        hand.Add(new OID(1, 1));
        hand.Add(new OID(3, 3));
        hand.Add(new OID(4, 4));
        hand.Add(new OID(1, 4));
        hand.Add(new OID(3, 1));
        hand.Add(new OID(1, 3));
        hand.Add(new OID(3, 4));
        
        group1.Add(new OID(1, 1));
        group1.Add(new OID(1, 2));
        group1.Add(new OID(1, 3));
        group1.Add(new OID(1, 4));
        
        group2.Add(new OID(3, 3));
        group2.Add(new OID(3, 4));
        group2.Add(new OID(3, 5));

        
        sortGroups.Add(group1);
        sortGroups.Add(group2);
        
        deadwood.Add(new OID(3, 1));
        deadwood.Add(new OID(2, 1));
        deadwood.Add(new OID(2, 4));
        deadwood.Add(new OID(4, 4));

        bool allGroupsLegit = true;
        
        (List<List<OID>> groups, List<OID> deadwood) sortedHand = runSort.Sort(hand);
        
        Assert.AreEqual(sortGroups.Count, sortedHand.groups.Count, "Group count is not equal");

        foreach (var expectedGroup in sortGroups)
        {
            bool groupFound = false;

            foreach (var actualGroup in sortedHand.groups)
            {
                if (actualGroup.Count == expectedGroup.Count)
                {
                    bool allOidsMatch = expectedGroup.All(expectedOid =>
                        actualGroup.Any(actualOid => actualOid.Equals(expectedOid))
                    );

                    if (allOidsMatch)
                    {
                        groupFound = true;
                        break;
                    }
                }
            }

            Assert.IsTrue(
                groupFound,
                $"Target group [{string.Join(", ", expectedGroup.Select(o => $"({o.ObjectID},{o.VariantID})"))}] is not found."
            );
        }

        Assert.AreEqual(
            deadwood.Count,
            sortedHand.deadwood.Count,
            "Deadwood count is not equal."
        );

        foreach (var expectedOid in deadwood)
        {
            bool found = sortedHand.deadwood.Any(actualOid => actualOid.Equals(expectedOid));
            Assert.IsTrue(
                found,
                $"Target deadwood item ({expectedOid.ObjectID},{expectedOid.VariantID}) is not found."
            );
        }
    }

    [Test]
    public void SmartSort_ShouldFindBestCombination()
    {
        List<OID> hand = new List<OID>();
        List<List<OID>> sortGroups = new List<List<OID>>();
        List<OID> group1 = new List<OID>();
        List<OID> group2 = new List<OID>();
        List<OID> group3 = new List<OID>();

        List<OID> deadwood = new List<OID>();
        
        hand.Add(new OID(2, 1));
        hand.Add(new OID(1, 2));
        hand.Add(new OID(3, 5));
        hand.Add(new OID(2, 4));
        hand.Add(new OID(1, 1));
        hand.Add(new OID(3, 3));
        hand.Add(new OID(4, 4));
        hand.Add(new OID(1, 4));
        hand.Add(new OID(3, 1));
        hand.Add(new OID(1, 3));
        hand.Add(new OID(3, 4));
        
        group1.Add(new OID(1, 1));
        group1.Add(new OID(1, 2));
        group1.Add(new OID(1, 3));
        
        group2.Add(new OID(1, 4));
        group2.Add(new OID(2, 4));
        group2.Add(new OID(4, 4));
        
        group3.Add(new OID(3, 3));
        group3.Add(new OID(3, 4));
        group3.Add(new OID(3, 5));
        
        sortGroups.Add(group1);
        sortGroups.Add(group2);
        sortGroups.Add(group3);
        
        deadwood.Add(new OID(2, 1));
        deadwood.Add(new OID(3, 1));


        bool allGroupsLegit = true;
        
        (List<List<OID>> groups, List<OID> deadwood) sortedHand = smartSort.Sort(hand);
        
        Assert.AreEqual(sortGroups.Count, sortedHand.groups.Count, "Group count is not equal");

        foreach (var expectedGroup in sortGroups)
        {
            bool groupFound = false;

            foreach (var actualGroup in sortedHand.groups)
            {
                if (actualGroup.Count == expectedGroup.Count)
                {
                    bool allOidsMatch = expectedGroup.All(expectedOid =>
                        actualGroup.Any(actualOid => actualOid.Equals(expectedOid))
                    );

                    if (allOidsMatch)
                    {
                        groupFound = true;
                        break;
                    }
                }
            }

            Assert.IsTrue(
                groupFound,
                $"Target group [{string.Join(", ", expectedGroup.Select(o => $"({o.ObjectID},{o.VariantID})"))}] is not found."
            );
        }

        Assert.AreEqual(
            deadwood.Count,
            sortedHand.deadwood.Count,
            "Deadwood count is not equal."
        );

        foreach (var expectedOid in deadwood)
        {
            bool found = sortedHand.deadwood.Any(actualOid => actualOid.Equals(expectedOid));
            Assert.IsTrue(
                found,
                $"Target deadwood item ({expectedOid.ObjectID},{expectedOid.VariantID}) is not found."
            );
        }
    }
}