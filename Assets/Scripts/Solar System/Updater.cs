
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Updater : MonoBehaviour
{
    private static List<ManagedUpdateBehaviour> updaterList = new List<ManagedUpdateBehaviour>();
    public static UnityEvent onUpdaterReset = new UnityEvent();

    private void Update()
    {
        if (updaterList.Count == 0)
            return;

        for (int i = 0; i < updaterList.Count; i++)
        {
            updaterList[i].OnUpdate();
        }
    }

    public static void AddToList<T>(T behavior) where T : ManagedUpdateBehaviour
    {
        updaterList.Add(behavior);
    }
    public static void RemoveFromList<T>(T behavior) where T : ManagedUpdateBehaviour
    {
        updaterList.Remove(behavior);
    }
    public static void ResetUpdater() 
    {
        updaterList.Clear();
        onUpdaterReset?.Invoke();
    }
}
