using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Interact : MonoBehaviour
{
    public LayerMask colliderMask = 8;
    bool interactable = false;
    public bool Interactable
    {
        get
        {
            if (UIController.Get.caller != gameObject)
                return false;
            if (ParentPerspective == GameController.Get.CurrentPerspective && interactable == true)
                return true;
            if (ParentPerspective == Perspective.None && interactable == true)
                return true;
            return false;
        }
        set
        {
            interactable = value;
            if (value == true)
            {
                UIController.Get.Interact(gameObject, value);
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

    private void Start()
    {
        Interactable = false;
    }
    Vector2 _ColliderSize;
    public Collider2D _Collider;
    private void FixedUpdate()
    {
        if (TryGetComponent<BoxCollider2D>(out BoxCollider2D box))
        {
            _ColliderSize = box.size;
            if (InteractableCheck(Physics2D.OverlapBox(transform.position, _ColliderSize, 0, colliderMask)))
                Interactable = true;
            else
                Interactable = false;
        }
    }
    private void Update()
    {
        if (Interactable && (Input.GetKeyDown(interactKey) || Input.GetKeyDown(interactKeyAlternative)))
        {
            interact?.Invoke(transform);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_ColliderSize.y != 0)
        {
            Gizmos.DrawCube(transform.position, _ColliderSize);
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, _ColliderSize.x);
        }
    }

    /*
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
    */

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
        _Collider = collision;
        if (!collision)
            return false;
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
