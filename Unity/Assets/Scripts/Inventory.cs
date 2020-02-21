using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Player player;
    public GameObject itembuttonPrefab;
    public GameObject itembutton;
    private bool PickUpAllowed
    {
        get
        {
            if (slot.item == null)
                return true;
            return false;
        }
    }
    public Slot slot;

    Item CheckItem()
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(transform.position, GetComponent<BoxCollider2D>().size, 0, Vector2.zero);

        if (raycastHit2Ds != null)
        {
            for (int i = 0; i < raycastHit2Ds.Length; i++)
            {
                    Debug.Log("Hola! "+ raycastHit2Ds[i].collider.name);
                if (raycastHit2Ds[i].collider.GetComponent<Item>())
                {
                    return raycastHit2Ds[i].collider.GetComponent<Item>();

                }
            }
        }
        Debug.Log("Kitos!");
        return null;
    }

    public void PickUpItem()
    {
        if (!PickUpAllowed)
            return;

        Item item = CheckItem();
        if (item == null)
            return;
        slot.item = item;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        //itembutton = Instantiate(itembuttonPrefab, inventory.slots[i].transform, false);
    }

    public void DropItem()
    {
        gameObject.GetComponent<Transform>().localPosition = player.GetComponent<Transform>().localPosition + Vector3.forward;
    }
}
