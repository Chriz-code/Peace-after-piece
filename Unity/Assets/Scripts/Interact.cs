using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Interact : MonoBehaviour
{
    bool interactable = false;
    public bool Interactable
    {
        get
        {
            if (ParentPerspective == GameController.Get.CurrentPerspective && interactable == true)
                return true;
            if (ParentPerspective == Perspective.None && interactable == true)
                return true;
            return false;
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
    [SerializeField] public InteractEvent interact;
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

    public KeyCode interactKey = KeyCode.E;
    public KeyCode interactKeyAlternative = KeyCode.Mouse0;


    private void Update()
    {
        if (Interactable && (Input.GetKeyDown(interactKey) || Input.GetKeyDown(interactKeyAlternative)))
        {
            interact?.Invoke(transform);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("Yare Yare You're now within my 2 meter range! " + ParentPerspective);
            Interactable = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("Yare Yare You're now within my 2 meter range! " + ParentPerspective);
            Interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("Yare Yare You're now outside my 2 meter range! " + ParentPerspective);
            Interactable = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (InteractableCheck(collision))
        {
            Debug.Log("Yare Yare You're now outside my 2 meter range! " + ParentPerspective);
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
[System.Serializable]
public class InteractEvent : UnityEvent<Transform> { }
