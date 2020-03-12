using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : PuzzelBase
{
    public Vector2 alternatePosition = Vector2.zero;
    public bool mustMatch = false;
    public Item lastHeldItem;
    [SerializeField] public Item matchItem = null;
    [SerializeField] Item heldItem = null;
    [SerializeField] public Item HeldItem
    {
        get
        {
            return heldItem;
        }
        set
        {
            if (heldItem != null)
                RevertToDefault(heldItem);

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

    public void RevertToDefault(Item item)
    {
        item.parent.GetComponent<SpriteRenderer>().sprite = item.defaultSprite;
        lastHeldItem = null;
    }

    public void SwitchSprite()
    {
        if (HeldItem != null)
        {
            HeldItem.parent.GetComponent<SpriteRenderer>().sprite = HeldItem.changeSprite;
        }
    }
}
