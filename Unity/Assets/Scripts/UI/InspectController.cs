using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectController : MonoBehaviour
{
    public GameObject panel = null;
    public UnityEngine.UI.Image image = null;
    public DialogCaller dialogCaller;

    public void InspectItem(Item item)
    {
        panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf)
        {
            image.sprite = Switch(item);
            if (!item)
                return;
            Dialog[] dialogChain = new Dialog[1];
            if (GameController.Get.CurrentPerspective == Perspective.Elenor)
            {
                dialogChain[0] = item.inspectDialogEleanor;

            }
            else if(GameController.Get.CurrentPerspective == Perspective.Angela)
            {
                dialogChain[0] = item.inspectDialogAngela;
            }
            UIController.Get.dialogController.StartDialog(dialogChain);
        }
        else
        {
            Debug.Log("No Object");
            image.sprite = null;
        }
    }
    public void InspectItem(GameObject gameObj)
    {
        panel.SetActive(!panel.activeSelf);
        if (panel.activeSelf)
        {
            image.sprite = gameObj.GetComponent<SpriteRenderer>().sprite;
            if (gameObj.GetComponent<DialogCaller>())
            {
                gameObj.GetComponent<DialogCaller>().CallDialog();
            }
            //image.SetNativeSize();
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
        panel.SetActive(false);
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
}
