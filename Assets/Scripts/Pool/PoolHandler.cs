using System.Collections.Generic;
using UnityEngine;

public class PoolHandler
{
    public Dictionary<OID, GenericObjectPool<Item>> itemPools;
    public GenericObjectPool<Item> itemPool;
    private Transform poolContainer;
    private GameItemsConfig gameItemsConfig;
    public PoolHandler()
    {
        itemPools = new Dictionary<OID, GenericObjectPool<Item>>();
    }
    
    public void Initialize(GameItemsConfig gameItemsConfig)
    {
        this.gameItemsConfig = gameItemsConfig;
        poolContainer = new GameObject("Pool").transform;
        Object.DontDestroyOnLoad(poolContainer);

        int warmCount = 0;
        

        foreach (ItemDataSerializer itemDataSerializer in gameItemsConfig.ItemDataSerializers)
        {
            warmCount += itemDataSerializer.ItemData.Count;
        }
        
        itemPool = new GenericObjectPool<Item>
        (
            gameItemsConfig.ItemDataSerializers[0].ItemData[0].Item,
            warmCount,
            poolContainer
        );
        
        itemPool.CreateInitialPoolObjects();
    }

    private GenericObjectPool<Item> GetItemPool(OID oid)
    {
        return itemPools[oid];
    }

    public Item GetItem(OID oid)
    {
        Item item = itemPool.Get();
        
        // better way datapass
        var data = gameItemsConfig.GetItemData(oid);
        item.gameObject.name = data.ItemName;
        item.OID = data.OID;
        item.UpdateGFX(data.ItemSprite);
        
        return item;
    }

    public void ReleaseItem(Item item)
    {
        itemPool.Release(item);
    }
    
    public void Clear()
    {
        itemPool.ReleaseAll();
    }
}