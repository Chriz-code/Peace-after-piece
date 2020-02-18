using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;
    private bool pickUpAllowed;

    private Inventory inventory;
    public GameObject itemButton;

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        //if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
            //PickUpItem();
    }

    public void PickUpItem()
    {
        if (!pickUpAllowed)
            return;

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
        }
    }

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
