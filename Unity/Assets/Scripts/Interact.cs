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
                UIController.Get.Interact(gameObject, interactable);
            }
        }
    }
    [SerializeField] public UnityEngine.Events.UnityEvent interact;
    public KeyCode interactKey = KeyCode.E;
    Perspective ParentPerspective
    {
        get
        {
            transform.root.TryGetComponent<ThisPerspective>(out ThisPerspective thisPerspective);
            if (thisPerspective)
                return thisPerspective.perspective;
            else
                return Perspective.None;
        }
    }

    private void Update()
    {
        if (interactable && Input.GetKeyDown(interactKey))
        {
            interact?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("You're inside my 2 meter range! " + ParentPerspective);
            Interactable = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("You're inside my 2 meter range! " + ParentPerspective);
            Interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("You're outside my 2 meter range! " + ParentPerspective);
            Interactable = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("You're outside my 2 meter range! " + ParentPerspective);
            Interactable = false;
        }
    }

    private void OnDestroy()
    {
        Interactable = false;
    }
    private void OnDisable()
    {
        Interactable = false;
    }

    bool InteractableCheck(Collider2D collision)
    {
        collision.TryGetComponent<Player>(out Player player);
        if (player && ParentPerspective == GameController.Get.CurrentPerspective)
        {
            return true;
        }
        if (player && ParentPerspective == Perspective.None)
        {
            return true;
        }
        return false;
    }
    bool InteractableCheck(Collision2D collision)
    {
        collision.transform.TryGetComponent<Player>(out Player player);
        if (player && ParentPerspective == GameController.Get.CurrentPerspective)
        {
            return true;
        }
        if (player && ParentPerspective == Perspective.None)
        {
            return true;
        }
        return false;
    }
}
