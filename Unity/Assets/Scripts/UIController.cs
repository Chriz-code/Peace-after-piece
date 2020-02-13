using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static UIController get;
    public static UIController Get { get { return get; } }

    private void Awake()
    {
        if (get != null && get != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            get = this;
        }
    }
}
