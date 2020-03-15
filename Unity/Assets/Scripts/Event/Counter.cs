using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public short maxCount = 1;
    short count = 0;

    [SerializeField] InteractEvent matchEvent = null;

    public void Add()
    {
        if (count >= maxCount)
            return;
        else
            count++;
        if (count >= maxCount)
            matchEvent?.Invoke(transform, null);
    }
}
