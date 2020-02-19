using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;
    public GameObject item;
    public GameObject player;

    private void Start()
    {
        inventory = player.GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount > 0)
        {
            inventory.isFull[i] = false;
        }

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    DropItem();
        //}
    }

    public void DropItem()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
