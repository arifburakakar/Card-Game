using System.Collections.Generic;

public interface ISort
{
    //sort groups and deadwood group
    (List<List<OID>> groups, List<OID> deadwood) Sort(List<OID> hand);
}