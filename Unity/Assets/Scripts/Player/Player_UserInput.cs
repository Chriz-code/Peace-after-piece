using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UserInput : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Player player = null;
    [SerializeField] Player_Movement _Movement = null;
    [SerializeField] Inventory inventory = null;

    [Header("Input")]
    [SerializeField] KeyCode inspectKey = KeyCode.C;
    [SerializeField] KeyCode inspectKeyAlternative = KeyCode.Mouse1;

    private void Start()
    {
        OnValidate();
    }
    private void OnValidate()
    {
        if (!player || !_Movement || !inventory)
        {
            TryGetComponent<Player>(out Player player);
            this.player = player;
            TryGetComponent<Player_Movement>(out Player_Movement _Movement);
            this._Movement = _Movement;
            TryGetComponent<Inventory>(out Inventory inventory);
            this.inventory = inventory;

            if (!player || !_Movement || !inventory)
            {
                Debug.LogWarning(this + " is missing refrences");
            }
        }
    }
    void Update()
    {
        if (GameController.Get.ComparePerspective(player.GetPerspective))
        {
            _Movement.Movement(Input.GetAxis("Horizontal"));
        }
        if (Input.GetKeyDown(inspectKey) || Input.GetKeyDown(inspectKeyAlternative))
        {
            InspectObjectInHand();
        }
    }

    private void OnDisable()
    {
        _Movement.Movement(0);
    }

    void InspectObjectInHand()
    {
        Debug.Log(inventory.slots[0].GetComponent<Slot>().item.name);
    }
}
