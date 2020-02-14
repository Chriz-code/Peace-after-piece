using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDetonation : MonoBehaviour
{
    public bool onCall = true;
    public float time = 2;
    // Start is called before the first frame update
    void Start()
    {
        if (onCall)
            return;
        Destroy(gameObject, time);
    }

    public void DestroyAfterT()
    {
        Destroy(gameObject, time);
    }
}
