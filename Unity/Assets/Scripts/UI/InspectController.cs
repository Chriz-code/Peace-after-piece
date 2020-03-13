using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectController : MonoBehaviour
{
    public GameObject panel = null;
    public UnityEngine.UI.Image image = null;

    public void InspectItem(Item item)
    {
        panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf)
        {
            image.sprite = Switch(item);
        }
        else
        {
            Debug.Log("No Object");
            image.sprite = null;
        }
    }


    Sprite Switch(Item item)
    {
        if (item)
            switch (item.itemType)
            {
                case ItemType.Book:
                    InspectBook(item);
                    return item.book;
                case ItemType.Key:
                    InspectKey(item);
                    return item.key;
                case ItemType.Painting:
                    InspectPainting(item);
                    return item.painting;
            }
        Debug.LogWarning("Unifentified Object");
        return null;
    }

    void InspectBook(Item item)
    {
        image.sprite = item.book;
    }
    void InspectKey(Item item)
    {
        image.sprite = item.key;

    }
    void InspectPainting(Item item)
    {
        image.sprite = item.painting;

    }
    public void InspectItem(Sprite sprite)
    {
        panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf)
        {
            image.sprite = sprite;
            //image.SetNativeSize();
        }
        else
        {
            Debug.Log("No Object");
            image.sprite = null;
        }
    }
}
