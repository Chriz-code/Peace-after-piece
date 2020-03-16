using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType { Book, Key, Painting }
public class Item : MonoBehaviour
{
    public Transform parent = null;
    public ItemType itemType = ItemType.Book;
    [Header("OverWorldSprites")]
    public Sprite defaultSprite = null;
    [SerializeField] private Sprite changeSprite = null;
    public Dialog inspectDialogAngela;
    public Dialog inspectDialogEleanor;

    public Sprite ChangeSprite
    {
        get
        {
            if (changeSprite == null)
                return defaultSprite;
            return changeSprite;
        }
    }

    [Header("InspectImage")]
    public Sprite book = null;
    public Sprite key = null;
    public Sprite painting = null;

    public bool firstPickup = true;
    [SerializeField] InteractEvent pickupEvent = null;

    public void CallEvent()
    {
        if (!firstPickup)
            return;

        pickupEvent?.Invoke(transform, null);
        firstPickup = false;
        print("First Pickup Event!!");
    }
}