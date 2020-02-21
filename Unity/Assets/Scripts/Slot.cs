using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item item;

    private void Start()
    {
   
    }

    private void Update()
    {
 
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
