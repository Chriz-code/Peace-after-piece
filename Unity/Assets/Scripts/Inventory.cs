using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Player player;
    public Slot slot;
    public bool PickUpAllowed
    {
        get
        {
            if (slot)
                if (slot.item == null && item != null)
                    return true;
            return false;
        }
    }
    public bool DropAllowed
    {
        get
        {
            if (slot)
                if (slot.item != null)
                    return true;
            return false;
        }
    }
    public LayerMask mask;
    public GameObject oppositeRoom;
    Item CheckItem()
    {
        Vector3 boxCastRayGrej = new Vector3(
            Mathf.Abs(GetComponent<BoxCollider2D>().size.x * transform.localScale.x),
            Mathf.Abs(GetComponent<BoxCollider2D>().size.y * transform.localScale.y),
            1);

        Collider[] collider2Ds = Physics.OverlapBox(transform.position, boxCastRayGrej, Quaternion.identity, mask);

        if (collider2Ds.Length > 0)
        {
            for (int i = 0; i < collider2Ds.Length; i++)
            {
                Debug.Log("Hola! " + collider2Ds[i].name);
                if (collider2Ds[i].GetComponent<Item>())
                {
                    return collider2Ds[i].GetComponent<Item>();
                }
            }
        }
        //Debug.Log("Kitos!");
        return null;
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

        if (ParentPerspective(gameObject.transform) == GameController.Get.CurrentPerspective)
        {
            return true;
        }
        if (ParentPerspective(gameObject.transform) == Perspective.None)
        {
            return true;
        }
        return false;
    }

    [Header("Debug")]
    public Item item = null;

    void OpenInteract(GameObject gameObject, bool open)
    {
        if (CheckPerspective(gameObject))
        {
            UIController.Get.Interact(gameObject, open);
            return;
        }
        UIController.Get.Interact(gameObject, false);
        return;
    }

    public void PickUpItem(Item item = null)
    {
        if (item != null)
        {
            slot.item = item;
            slot.GetComponent<UnityEngine.UI.Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            item.GetComponent<Transform>().localPosition = new Vector2(222, 222);
            return;
        }
        if (PickUpAllowed == false)
            return;

        slot.item = this.item;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = this.item.GetComponent<SpriteRenderer>().sprite;
        this.item.GetComponent<Transform>().localPosition = new Vector2(222, 222);
        //itembutton = Instantiate(itembuttonPrefab, inventory.slots[i].transform, false);
    }

    public void PlaceItem(Transform transform)
    {
        if (transform.TryGetComponent<ItemHolder>(out ItemHolder itemHolder))
        {
            if (itemHolder.item)
            {
                PickUpItem(itemHolder.item);
                itemHolder.item = null;
                return;
            }
            else
            {
                if (!DropAllowed)
                    return;
                itemHolder.item = slot.item;
            }
        }

        if (!DropAllowed)
            return;

        Vector3 newPosition = transform.GetComponent<Transform>().localPosition;
        newPosition.z = -1;

        slot.item.GetComponent<Transform>().localPosition = newPosition;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        slot.item = null;

    }
    public void DropItem()
    {
        if (!DropAllowed)
            return;
        Vector3 newPosition = transform.GetComponent<Transform>().localPosition;
        newPosition.z = -1;

        slot.item.GetComponent<Transform>().localPosition = newPosition;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        slot.item = null;
    }


    public void TransferItem(Transform transform)
    {
        if (!DropAllowed)
            return;

        Vector3 newPosition = this.transform.GetComponent<Transform>().localPosition;
        newPosition.z = -1;
        newPosition.x += 1;

        slot.item.transform.parent = oppositeRoom.transform;
        slot.item.GetComponent<Transform>().localPosition = newPosition;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;
        slot.item = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>() && CheckPerspective(collision.gameObject))
        {
            item = collision.GetComponent<Item>();
            OpenInteract(collision.gameObject, true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>() && CheckPerspective(collision.gameObject))
        {
            item = null;
            OpenInteract(collision.gameObject, false);
        }
    }
}