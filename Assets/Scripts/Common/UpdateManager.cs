using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : GenericSingleton<UpdateManager>
{
    private List<ManagedUpdateBehaviour> updaterList = new List<ManagedUpdateBehaviour>();

    public override void Awake()
    {
        base.Awake();
    }

    public void AddToList<T>(T objectToAdd) where T : ManagedUpdateBehaviour
    {
        updaterList.Add(objectToAdd);
    }

    public void RemoveFromList<T>(T objectToRemove) where T : ManagedUpdateBehaviour
    {
        updaterList.Remove(objectToRemove);
    }


    private void Update()
    {
        if (updaterList.Count == 0)
            return;

        for (int i = 0; i < updaterList.Count; i++)
        {
            updaterList[i].OnUpdate();
        }
   }
}
