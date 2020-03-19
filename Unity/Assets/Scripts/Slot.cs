using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] Item tempItem;
    [SerializeField] Item item;
    public Item ItemSlot
    {
        get
        {
            return item;
        }
        set
        {
            if (value == null && item != null)
            {
                if (GetComponent<Dissolve_Burn>())
                {
                    GetComponent<Dissolve_Burn>().BurnObject(1);
                    item.parent.GetComponent<Dissolve_Burn>().BurnObject(0);
                }
                else if (GetComponent<RippleAtPoint>())
                {
                    StartCoroutine(ItemSetAnimationUnRipple(item));
                }
                //GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
                item = value;

            }
            else
            {
                GetComponent<UnityEngine.UI.Image>().color = Color.white;
                GetComponent<UnityEngine.UI.Image>().sprite = value.parent.GetComponent<SpriteRenderer>().sprite;

                if (AttemptDissolveAnimation(value)) { }
                else if (AttemptRippleAnimation(value)) { }
                else
                {
                    item = value;
                    item.parent.localPosition = new Vector2(222, 222);
                    item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    item.GetComponent<Collider2D>().enabled = true;
                    tempItem = null;
                    ienumerating = null;
                }
            }

        }
    }

    IEnumerator ienumerating = null;
    IEnumerator ItemSetAnimationBurn(Item item)
    {
        tempItem = item;

        Dissolve_Burn dissolve = item.parent.GetComponent<Dissolve_Burn>();
        dissolve.BurnObject(1);
        GetComponent<Dissolve_Burn>().BurnObject(0);
        float timeStart = Time.time;
        yield return new WaitUntil(() => (dissolve.burnComplete == true));

        StopBurn(item);
    }
    IEnumerator ItemSetAnimationRipple(Item item)
    {
        tempItem = item;
        RippleAtPoint ripplePoint = GetComponent<RippleAtPoint>();
        ripplePoint.Ripple(item.transform);
        yield return new WaitUntil(() => ripplePoint.ReplaceTransparency(item.parent.GetComponent<SpriteRenderer>(), GetComponent<UnityEngine.UI.Image>()) <= 0);
        StopRipple(item);
    }

    IEnumerator ItemSetAnimationUnRipple(Item item)
    {
        tempItem = item;
        RippleAtPoint ripplePoint = GetComponent<RippleAtPoint>();
        ripplePoint.Ripple(transform);

        yield return new WaitUntil(() => ripplePoint.ReplaceTransparency(item.parent.GetComponent<SpriteRenderer>(), GetComponent<UnityEngine.UI.Image>(), true) >= 1);

    }

    private void Start()
    {
        if (GetComponent<Dissolve_Burn>())
        {
            GetComponent<Dissolve_Burn>().DissolveAmount = 1;
        }
    }
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().onChangePerspective += StopIEnumerating;
    }
    private void OnDisable()
    {
        GameController.Get.onChangePerspective -= StopIEnumerating;
    }

    bool AttemptDissolveAnimation(Item value)
    {
        if (GetComponent<Dissolve_Burn>())
        {
            if (value.parent.GetComponent<Dissolve_Burn>() && ienumerating == null)
            {
                ienumerating = ItemSetAnimationBurn(value);
                StartCoroutine(ienumerating);
                return true;
            }
        }
        return false;
    }
    bool AttemptRippleAnimation(Item value)
    {
        if (GetComponent<RippleAtPoint>())
        {
            if (ienumerating == null)
            {
                ienumerating = ItemSetAnimationRipple(value);
                StartCoroutine(ienumerating);
            }
        }
        return false;
    }

    void StopIEnumerating(GameController gc, Perspective perspective)
    {
        if (ienumerating != null)
        {
            StopCoroutine(ienumerating);
            if (GetComponent<Dissolve_Burn>())
                StopBurn(tempItem);
            if (GetComponent<RippleAtPoint>())
                StopRipple(tempItem);
        }
    }
    void StopBurn(Item item)
    {
        //print("Burn Interrupted");
        item.parent.localPosition = new Vector2(222, 222);
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        item.GetComponent<Collider2D>().enabled = true;
        GetComponent<Dissolve_Burn>().BurnObject(0);

        this.item = item;
        tempItem = null;
        ienumerating = null;
    }
    void StopRipple(Item item)
    {
        // print("Dripple Interuppted");
        item.parent.localPosition = new Vector2(222, 222);
        item.parent.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        item.GetComponent<Collider2D>().enabled = true;

        Color itemColor = item.parent.GetComponent<SpriteRenderer>().color;
        itemColor.a = 1;
        //item.parent.GetComponent<SpriteRenderer>().color = itemColor;

        this.item = item;
        tempItem = null;
        ienumerating = null;
    }
}