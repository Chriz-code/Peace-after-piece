using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item item;

  
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
