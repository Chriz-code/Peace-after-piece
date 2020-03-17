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

    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += CheckItemOverlap;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= CheckItemOverlap;
    }
    void CheckItemOverlap(GameController gc, Perspective perspective)
    {
        if (perspective == GetComponent<ThisPerspective>().perspective)
        {
            if (TryGetComponent<BoxCollider2D>(out BoxCollider2D box))
            {
                Vector2 _ColliderSize = box.size;
                Collider2D _Collider = Physics2D.OverlapBox(transform.position, _ColliderSize, 0, LayerMask.GetMask("Item"));
                if (_Collider != null && ParentPerspective(_Collider.transform) == perspective)
                {
                    UIController.Get.Interact(_Collider.gameObject, true);
                }
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
                    {
                        print("Pickup was not allowed");
                        return false;
                    }

                    return true;
                }
            }
            print("Pickup was not allowed");
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
            print("Drop was not allowed");
            return false;
        }
    }

    #region Collision
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
    #endregion

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
            slot.GetComponent<UnityEngine.UI.Image>().sprite = item.defaultSprite;


            item.CallEvent();
            return;
        }
        if (PickUpAllowed == false)
            return;

        item = this.CurrentColItem;
        slot.ItemSlot = item;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = item.defaultSprite;

        item.CallEvent();
    }
    public void DropItem()
    {
        if (!DropAllowed)
            return;
        Vector3 newPosition = transform.GetComponent<Transform>().localPosition;
        newPosition.z = -1;

        Item item = slot.ItemSlot;
        slot.ItemSlot = null;

        item.parent.GetComponent<Collider2D>().enabled = true;
        item.GetComponent<Collider2D>().enabled = true;
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        item.parent.localPosition = newPosition;
        //slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
    }
    public void PlaceItem(Transform caller, Transform sender)
    {
        if (caller.TryGetComponent<ItemHolder>(out ItemHolder itemHolder))
        {
            if (itemHolder.HeldItem && slot.ItemSlot == null) // PickUp if there's already an item
            {
                PickUpItem(itemHolder.HeldItem);
                itemHolder.HeldItem = null;
                return;
            }
            else //Place Item in Holder
            {
                if (!DropAllowed)
                    return;
                if (itemHolder.HeldItem != null)
                    return;
                if (itemHolder.mustMatch)
                    if (itemHolder.matchItem != slot.ItemSlot)
                        return;


                itemHolder.HeldItem = slot.ItemSlot;
                Vector3 newPosition = caller.position;

                if (itemHolder.alternatePosition != Vector2.zero)
                    newPosition = caller.position + (Vector3)itemHolder.alternatePosition;

                Item item = slot.ItemSlot;
                item.GetComponent<Collider2D>().enabled = false;
                item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

                item.parent.position = newPosition;
                newPosition = item.parent.position;
                newPosition.z = -1;
                item.parent.localPosition = newPosition;

                //slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
                slot.ItemSlot = null;
            }
        }
    }
    public void TransferItem(Transform caller, Transform sender)
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
        //slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        slot.ItemSlot = null;
    }

    #endregion
}