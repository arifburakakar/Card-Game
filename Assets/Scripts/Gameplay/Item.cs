using PrimeTween;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPoolable
{
    public OID OID;
    public SpriteRenderer GFX; // change with sprite
    public Sprite defaultSprite;
    public Sprite backgroundSprite;

    private Tween scaleTween;
    
    public void Create()
    {
        
    }

    public void Spawn()
    {
        OnSpawn();
    }

    public virtual void Select()
    {
        
    }

    public virtual void Deselect()
    {

    }

    protected virtual void OnSpawn()
    {
        
    }

    public void UpdateGFX(Sprite sprite)
    {
        defaultSprite = sprite;
    }

    public void SetRotation(Vector3 rotation)
    {
        transform.eulerAngles = rotation;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void ScaleAnimation(float scale, float duration)
    {
        scaleTween.Complete();
        scaleTween = Tween.Scale(transform, scale, duration);
    }

    public void Despawn()
    {
        OnDespawn();
    }

    protected virtual void OnDespawn()
    {
        // return to pool
        
    }
}