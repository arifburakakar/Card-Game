using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : IManager
{
    protected Scene scene;
    public GameObject RootObject { get; set; }

    public void Initialize(Scene scene)
    {
        this.scene = scene;
        RootObject = new GameObject("Root");
        SceneManager.MoveGameObjectToScene(RootObject, scene);
        OnInitialize();
    }

    public virtual void CreateManagerNecessary()
    {
        
    }


    public void Enable()
    {
        RootObject.SetActive(true);
        OnEnable();
    }

    public void Execute()
    {
        OnExecute();
    }

    public void Disable()
    {
        RootObject.SetActive(false);
        OnDisable();
    }

    protected virtual void OnInitialize()
    {
        
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }

    public virtual void OnExecute()
    {
        
    }
}