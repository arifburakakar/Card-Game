using UnityEngine;

public class Item : MonoBehaviour, IPoolable
{
    public OID OID;
    [SerializeField]
    private SpriteRenderer gfx;
    public void Create()
    {
        
    }

    public void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateGFX(Sprite sprite)
    {
        gfx.sprite = sprite;
    }

    public void Despawn()
    {
        throw new System.NotImplementedException();
    }

    public void OnDespawn()
    {
        throw new System.NotImplementedException();
    }
}