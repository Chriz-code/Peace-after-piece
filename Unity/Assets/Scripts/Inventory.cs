using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Player player;
    public Slot slot;
    public GameObject oppositeRoom;
    [Header("Debug")]
    [SerializeField] List<Item> colItems = new List<Item>();
    Item CurrentColItem
    {
        get
        {
            if (colItems.Count > 0)
            {
                foreach (var item in colItems)
                {
                    if (item.gameObject == UIController.Get.Caller)
                        return item;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }

    public bool PickUpAllowed
    {
        get
        {
            if (slot)
            {
                if (slot.ItemSlot == null && CurrentColItem != null)
                {
                    if (UIController.Get.Caller != CurrentColItem.gameObject)
                        return false;

                    return true;
                }
            }
            return false;
        }
    }
    public bool DropAllowed
    {
        get
        {
            if (slot)
                if (slot.ItemSlot != null)
                    return true;
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckPerspective(collision.gameObject) && collision.GetComponent<Item>())
        {
            colItems.Add(collision.GetComponent<Item>());
            UIController.Get.Interact(collision.gameObject, true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckPerspective(collision.gameObject) && collision.GetComponent<Item>())
        {
            colItems.Remove(collision.GetComponent<Item>());
            UIController.Get.Interact(collision.gameObject, false);
        }
    }

    Perspective ParentPerspective(Transform transform)
    {
        transform.root.TryGetComponent<ThisPerspective>(out ThisPerspective thisPerspective);
        if (thisPerspective)
            return thisPerspective.perspective;
        else
            return Perspective.None;
    }
    bool CheckPerspective(GameObject gameObject)
    {
        if (TryGetComponent<ThisPerspective>(out ThisPerspective playerPerspective) && playerPerspective.perspective == GameController.Get.CurrentPerspective)
        {
            if (ParentPerspective(gameObject.transform) == GameController.Get.CurrentPerspective)
            {
                return true;
            }
            if (ParentPerspective(gameObject.transform) == Perspective.None)
            {
                return true;
            }
        }
        return false;
    }

    #region InventoryActions
    public void PickUpItem(Item item = null)
    {

        if (item != null && slot.ItemSlot == null) //If item is taken from an itemHolder
        {
            slot.ItemSlot = item;
            slot.GetComponent<UnityEngine.UI.Image>().sprite = item.parent.GetComponent<SpriteRenderer>().sprite;
            item.parent.localPosition = new Vector2(222, 222);
            item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            item.GetComponent<Collider2D>().enabled = true;
            return;
        }
        if (PickUpAllowed == false)
            return;

        item = this.CurrentColItem;
        slot.ItemSlot = item;

        slot.GetComponent<UnityEngine.UI.Image>().sprite = item.parent.GetComponent<SpriteRenderer>().sprite;
        item.parent.localPosition = new Vector2(222, 222);
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        item.GetComponent<Collider2D>().enabled = true;
    }
    public void PlaceItem(Transform transform)
    {
        if (transform.TryGetComponent<ItemHolder>(out ItemHolder itemHolder))
        {
            if (itemHolder.HeldItem && slot.ItemSlot == null) // PickUp
            {
                PickUpItem(itemHolder.HeldItem);
                itemHolder.HeldItem = null;
                return;
            }
            else
            {
                if (!DropAllowed)
                    return;
                if (itemHolder.HeldItem != null)
                    return;

                itemHolder.HeldItem = slot.ItemSlot;
            }
        }

        if (!DropAllowed)
            return;


        Vector3 newPosition = transform.position;

        Item item = slot.ItemSlot;
        item.GetComponent<Collider2D>().enabled = false;
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        item.parent.position = newPosition;
        newPosition = item.parent.position;
        newPosition.z = -1;
        item.parent.localPosition = newPosition;

        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        //print("Place");
        slot.ItemSlot = null;

    }
    public void DropItem()
    {
        if (!DropAllowed)
            return;
        Vector3 newPosition = transform.GetComponent<Transform>().localPosition;
        newPosition.z = -1;

        Item item = slot.ItemSlot;

        item.GetComponent<Collider2D>().enabled = true;
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        item.parent.localPosition = newPosition;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        slot.ItemSlot = null;
    }
    public void TransferItem(Transform transform)
    {
        if (!DropAllowed)
            return;

        Vector3 newPosition = this.transform.GetComponent<Transform>().localPosition;
        newPosition.z = -1;
        newPosition.x += 1;

        Item item = slot.ItemSlot;
        item.GetComponent<Collider2D>().enabled = true;
        item.parent.parent = oppositeRoom.transform;
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        item.parent.GetComponent<Transform>().localPosition = newPosition;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        slot.ItemSlot = null;
    }

    #endregion
}