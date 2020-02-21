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
            {
                Debug.Log("HEJ");
                return true;
            }
            return false;
        }
    }
    public Slot slot;
    public LayerMask mask;
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
                //Debug.Log("Hola! " + collider2Ds[i].name);
                if (collider2Ds[i].GetComponent<Item>())
                {
                    return collider2Ds[i].GetComponent<Item>();
                }
            }
        }
        //Debug.Log("Kitos!");
        return null;
    }

    public void PickUpItem()
    {


        if (PickUpAllowed == false)
            return;

        //Item item = CheckItem();
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


    public Item item = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {
            item = collision.GetComponent<Item>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {
            item = null;
        }
    }



}