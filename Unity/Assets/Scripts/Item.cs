using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //public GameObject player;
    private bool pickUpAllowed;

    //private Inventory inventory;
    //public GameObject itemButton;

    //private void Start()
    //{
    //    inventory = player.GetComponent<Inventory>();
    //}

    //private void Update()
    //{
    //  if (Input.GetKeyDown(KeyCode.Q))
    //DropItem();

    //    }

    //public void PickUpItem()
    //{
    //    if (!pickUpAllowed)
    //        return;

    //    for (int i = 0; i < inventory.slots.Length; i++)
    //    {
    //        if (inventory.isFull[i] == false)
    //        {
    //            inventory.isFull[i] = true;
    //            Instantiate(itemButton, inventory.slots[i].transform, false);
    //            gameObject.GetComponent<Transform>().localPosition = new Vector2(222, 222);
    //            //Destroy(gameObject);
    //            break;
    //        }
    //    }
    //}

    //public void DropItem()
    //{
    //    gameObject.GetComponent<Transform>().localPosition = player.GetComponent<Transform>().localPosition + Vector3.forward;
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            pickUpAllowed = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            pickUpAllowed = false;
    }
}
