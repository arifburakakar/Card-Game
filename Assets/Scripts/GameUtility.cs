using System;
using System.Collections.Generic;
using Random = System.Random;

public static class GameUtility
{
    public static void Shuffle<T>(this IList<T> list)
    {
        var rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void ShuffleStack<T>(this Stack<T> stack)
    {
        List<T> list = new List<T>(stack);
        list.Shuffle();
        stack.Clear();
        foreach (var item in list)
        {
            stack.Push(item);
        }
    }

    public static void ShuffleQueue<T>(this Queue<T> queue)
    {
        List<T> list = new List<T>(queue);
        list.Shuffle();
        queue.Clear();
        foreach (var item in list)
        {
            queue.Enqueue(item);
        }
    }
    
    public static int Sign(float value)
    {
        if (value > 0) return 1;
        if (value < 0) return -1;
        return 0;
    }

    public static long GetUtcTimeMilliseconds()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(); 
    }

    public static long GetUtcTimeSeconds()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds(); 
    }

    public static DateTime GetDatetimeFromUtcTimeMilliseconds(long utcTimeMilliseconds)
    {
        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        start = start.AddSeconds(utcTimeMilliseconds / 1000);
        return start;
    }

    public static DateTime GetDatetimeFromUtcTimeSeconds(long utcTimeSeconds)
    {
        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        start = start.AddSeconds(utcTimeSeconds);
        return start;
    }    

    public static DateTime GetDatetimeFromLocalTimeSeconds(long localSeconds)
    {
        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        start = start.AddSeconds(localSeconds);
        return start;
    }

    public static float MapValue(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
    
    public static void GCCollectManuel()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        GarbageCollector.GCMode = GarbageCollector.Mode.Manual;
#endif
    }

    public static void GCCollectDefault()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        GC.Collect(0, GCCollectionMode.Default);
#endif
    }

    public static void GCCollectForced()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        GC.Collect(0, GCCollectionMode.Forced);
        Debug.Log("GC Collect Forced");
#endif
    }

    public static void GCCollectOptimize()
    {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        GC.Collect(0, GCCollectionMode.Optimized);
        Debug.Log("GC Collect Optimize");
#endif
    }
}
