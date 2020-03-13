using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelBase : MonoBehaviour
{
    public InteractEvent matchEvent;
    bool match = false;
    public bool Match
    {
        get
        {
            return match;
        }
        set
        {
            match = value;
            if (match == true)
                matchEvent?.Invoke(transform, null);
        }
    }
}
