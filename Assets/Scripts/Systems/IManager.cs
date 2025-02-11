using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IManager
{
    GameObject RootObject { get;  set; }
    
    void Initialize(Scene scene);

    void CreateManagerNecessary();

    void Enable();

    void Execute();

    void Disable();
}