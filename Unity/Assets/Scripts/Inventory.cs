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
        item.GetComponent<Transform>().localPosition = new Vector2(222, 222);
        //itembutton = Instantiate(itembuttonPrefab, inventory.slots[i].transform, false);
    }


    public GameObject placeholder;
    public void DropItem()
    {

        item.GetComponent<Transform>().localPosition = placeholder.GetComponent<Transform>().localPosition;

        slot.item = null;
        slot.GetComponent<UnityEngine.UI.Image>().sprite = null;



    }

    public GameObject oppositeRoom;
    public void TransferItem()
    {
        item.transform.parent = oppositeRoom.transform;
        item.GetComponent<Transform>().localPosition = new Vector3(-13f, -3f, -0.74f);

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
            //item = null;
            item = collision.GetComponent<Item>();
        }
    }



}