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
    private Sprite changeSprite = null;
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

    
}
