using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Sprite open;
    public Sprite closed;

    public void SwitchSprite()
    {
        Debug.Log("opendoor!");

        if (GetComponent<SpriteRenderer>().sprite == closed)
            GetComponent<SpriteRenderer>().sprite = open;     
        if(GetComponent<SpriteRenderer>().sprite == open)
            GetComponent<SpriteRenderer>().sprite = closed;

    }
}
