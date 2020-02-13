using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Interact : MonoBehaviour
{
    bool interactable;
    [SerializeField] public UnityEngine.Events.UnityEvent interact;

    private void Update()
    {
        if (interactable)
        {
            interact?.Invoke();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<Player>())
        interactable = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Player>())
            interactable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Player>())
            interactable = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Player>())
            interactable = false;
    }
}
