using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interact : MonoBehaviour
{
    bool interactable = false;
    public bool Interactable
    {
        get
        {
            return interactable;
        }
        set
        {
            if (interactable != value)
            {
                interactable = value;
                UIController.Get.Interact(interactable);
            }
        }
    }



    [SerializeField] public UnityEngine.Events.UnityEvent interact;
    public KeyCode interactKey = KeyCode.E;


    private void Update()
    {
        if (interactable && Input.GetKeyDown(interactKey))
        {
            interact?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>())
            Interactable = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Player>())
            Interactable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>())
            Interactable = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Player>())
            Interactable = false;

    }
}
