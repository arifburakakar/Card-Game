using System.Collections;
using UnityEngine;
public class CoroutineHandler : MonoBehaviour
{
    public static CoroutineHandler Instance;
    public delegate void Callback();
    
    public void OnCreate()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public Coroutine Begin(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public Coroutine BeginWithCallback(float duration, Callback callback)
    {
        return Begin(CoroutineWithCallback(duration, callback));
    }
    
    public IEnumerator CoroutineWithCallback(float duration, Callback callback)
    {
        yield return new WaitForSeconds(duration);

        callback?.Invoke();
    }
    
    public void Kill(Coroutine coroutine)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}