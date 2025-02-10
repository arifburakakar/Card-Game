using System.Collections.Generic;
using UnityEngine;

public class PoolHandler
{
    public Dictionary<OID, GenericObjectPool<Item>> itemPools;

    private Transform poolContainer;
    public PoolHandler()
    {
        itemPools = new Dictionary<OID, GenericObjectPool<Item>>();
    }
    
    public void Initialize(GameItemsConfig gameItemsConfig)
    {
        poolContainer = new GameObject("Pool").transform;
        Object.DontDestroyOnLoad(poolContainer);

        foreach (ItemDataSerializer itemDataSerializer in gameItemsConfig.ItemDataSerializers)
        {
            foreach (ItemData itemData in itemDataSerializer.ItemData)
            {
                Item item = itemData.Item;
                item.gameObject.name = itemData.ItemName;
                item.OID = itemData.OID;
                item.UpdateGFX(itemData.ItemSprite);
                GenericObjectPool<Item> pool = new GenericObjectPool<Item>
                (
                    itemData.Item,
                    itemData.WarmCount,
                    poolContainer
                );

                itemPools.Add(itemData.OID, pool);
                pool.CreateInitialPoolObjects();
            }
        }
    }

    private GenericObjectPool<Item> GetItemPool(OID oid)
    {
        return itemPools[oid];
    }

    public Item GetItem(OID oid)
    {
        return GetItemPool(oid).Get();
    }

    public void ReleaseItem(Item item)
    {
        itemPools[item.OID].Release(item);
    }
    
    public void Clear()
    {
        foreach (var pools in itemPools.Values)
        {
            pools.ReleaseAll();
        }
    }
}