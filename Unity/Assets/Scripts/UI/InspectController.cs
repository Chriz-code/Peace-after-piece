using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectController : MonoBehaviour
{
    public GameObject panel = null;
    public UnityEngine.UI.Image image = null;

    public void Inspect(Item item)
    {
        panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf)
            image.sprite = Switch(item);
        else
            image.sprite = null;
    }


    Sprite Switch(Item item)
    {
        if (item)
            switch (item.itemType)
            {
                case ItemType.Book:
                    return item.book;
                case ItemType.Key:
                    return item.key;
                case ItemType.Painting:
                    return item.painting;
            }
        return null;
    }
}
