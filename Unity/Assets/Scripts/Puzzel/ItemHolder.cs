using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : PuzzelBase
{
    [SerializeField] public Item matchItem = null;
    [SerializeField] public Item heldItem = null;
    [SerializeField] public Item HeldItem
    {
        get
        {
            return heldItem;
        }
        set
        {
            heldItem = value;
            Match = false;

            if (heldItem == matchItem)
            {
                Match = true;
                if (heldItem != null)
                {
                    matchEvent?.Invoke(transform);
                }
            }
        }
    }

    public void SwitchSprite(Sprite sprite)
    {
       if (heldItem == true)
       {
            heldItem.parent.GetComponent<SpriteRenderer>().sprite = sprite;
       }
    }
}
