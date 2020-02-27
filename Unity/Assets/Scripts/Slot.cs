using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    Item item;
    public Item ItemSlot
    {
        get
        {
            return item;
        }
        set
        {
            if (value == null)
                GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
            else
                GetComponent<UnityEngine.UI.Image>().color = Color.white;
            item = value;
        }
    }

  
    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
