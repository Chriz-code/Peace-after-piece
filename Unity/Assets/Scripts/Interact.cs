using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Interact : MonoBehaviour
{
    [SerializeField] public InteractEvent interactEvent;
    bool interactable = false;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode interactKeyAlternative = KeyCode.Mouse0;
    public Vector2 localOffset = Vector2.zero;
    Vector2 colliderSize;
    [Header("Debug")]
    [SerializeField] public Collider2D currentCollision;

    public bool Interactable
    {
        get
        {
            if (UIController.Get.Caller != gameObject)
                return false;
            if (ParentPerspective == GameController.Get.CurrentPerspective && interactable == true)
            {
                return true;
            }
            if (ParentPerspective == Perspective.None && interactable == true)
            {
                return true;
            }
            return false;
        }
        set
        {
            interactable = value;
            UIController.Get.Interact(gameObject, localOffset, interactable);

        }
    }
    Perspective ParentPerspective
    {
        get
        {
            transform.root.TryGetComponent<ThisPerspective>(out ThisPerspective thisPerspective);
            if (thisPerspective)
            {
                return thisPerspective.perspective;
            }
            else
            {
                return Perspective.None;
            }
        }
    }


    private void Start()
    {
        Interactable = false;
    }
    private void Update()
    {
        if ((Input.GetKeyDown(interactKey) || Input.GetKeyDown(interactKeyAlternative)))
        {
            if (Interactable)
                interactEvent?.Invoke(transform);
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

    #region Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InteractableCheck(collision) && this.enabled)
        {
            Interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InteractableCheck(null) && this.enabled)
        {
            Interactable = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (InteractableCheck(collision.collider) && this.enabled)
        {
            Interactable = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (InteractableCheck(null) && this.enabled)
        {
            Interactable = false;
        }
    }
    #endregion
    bool InteractableCheck(Collider2D collision)
    {
        currentCollision = collision;
        if (!collision)
        {
            Interactable = false;
            return false;
        }
        if (!currentCollision)
        {
            Interactable = false;
            return false;
        }

        currentCollision.TryGetComponent<Player>(out Player player);
        if (!player)
        {
            Interactable = false;
            return false;
        }

        if (player.TryGetComponent<ThisPerspective>(out ThisPerspective playerPerspective))
        {
            if (player.userInput.enabled)
            {
                if (playerPerspective.perspective == GameController.Get.CurrentPerspective && playerPerspective)
                {
                    if (ParentPerspective == GameController.Get.CurrentPerspective)
                    {
                        return true;
                    }
                    if (ParentPerspective == Perspective.None)
                    {
                        return true;
                    }
                }
            }
        }

        Interactable = false;
        return false;
    }

}
[System.Serializable]
public class InteractEvent : UnityEvent<Transform> { }
