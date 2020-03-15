using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UserInput : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Player player = null;
    [SerializeField] Player_Movement movement = null;
    [SerializeField] Inventory inventory = null;

    [Header("Input")]
    [SerializeField] KeyCode inspectKey = KeyCode.C;
    [SerializeField] KeyCode inspectKeyAlternative = KeyCode.Mouse1;
    [SerializeField] KeyCode pickUpKey = KeyCode.E;
    [SerializeField] KeyCode pickUpKeyAlternative = KeyCode.Mouse0;
    [SerializeField] KeyCode dropKey = KeyCode.Q;
    [SerializeField] KeyCode dropKeyAlternative = KeyCode.Mouse2;

    private void Start()
    {
        OnValidate();
    }
    private void OnValidate()
    {
        if (!player || !movement || !inventory)
        {
            TryGetComponent<Player>(out Player player);
            this.player = player;
            TryGetComponent<Player_Movement>(out Player_Movement _Movement);
            this.movement = _Movement;
            TryGetComponent<Inventory>(out Inventory inventory);
            this.inventory = inventory;

            if (!player || !_Movement || !inventory)
            {
                Debug.LogWarning(this + " is missing refrences");
            }
        }
    }
    public bool frozen = false;
    void Update()
    {
        if (GameController.Get.ComparePerspective(player.GetPerspective))
        {
            if (Input.GetKeyDown(inspectKey) || Input.GetKeyDown(inspectKeyAlternative))
            {
                InspectObjectInHand();
            }
            if (!frozen)
            {
                if (!UIController.Get.inspectController || (!UIController.Get.inspectController.panel.activeSelf))
                {
                    movement.Movement(Input.GetAxis("Horizontal"));
                    if ((Input.GetKeyDown(pickUpKey) || Input.GetKeyDown(pickUpKeyAlternative)) && inventory.PickUpAllowed)
                    {
                        inventory.PickUpItem();
                    }
                    if ((Input.GetKeyDown(dropKey) || Input.GetKeyDown(dropKeyAlternative)) && inventory.DropAllowed)
                    {
                        inventory.DropItem();
                    }
                }
            }
        }
        if (frozen)
        {
            movement.Movement(0);
        }
    }

    private void OnDisable()
    {
        movement.Movement(0);
    }

    void InspectObjectInHand()
    {
        if (UIController.Get.inspectController && inventory)
        {
            UIController.Get.inspectController.InspectItem(inventory.slot.ItemSlot);
        }
    }
}
