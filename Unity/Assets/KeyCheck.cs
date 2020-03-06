using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCheck : MonoBehaviour
{
    public Slot slot;

    public void CheckInventoryItem()
    {
        if (!GetComponent<Interact>())
            return;
        if (!GetComponent<Interact>().currentCollision.GetComponent<Inventory>())
            return;

        Inventory inventory = GetComponent<Interact>().currentCollision.GetComponent<Inventory>();
        if (!inventory.DropAllowed)
            return;

        Item item = inventory.slot.ItemSlot;
        if (item.itemType != ItemType.Key)
            return;


        Debug.Log("OpenDoor");


        Vector3 newPosition = transform.position;

        item.GetComponent<Collider2D>().enabled = false;
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        item.parent.position = newPosition;
        newPosition = item.parent.position;
        newPosition.z = -1;
        item.parent.localPosition = newPosition;

        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        slot.ItemSlot = null;

        //TODO open door
    }


}
