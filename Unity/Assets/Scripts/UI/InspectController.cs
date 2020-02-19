using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectController : MonoBehaviour
{
    public void Inpect(Item item)
    {
        Debug.Log(item.name);
    }
}
