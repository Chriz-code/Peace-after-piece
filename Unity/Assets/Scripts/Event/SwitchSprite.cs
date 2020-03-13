using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
    [SerializeField] Sprite[] sprites = new Sprite[0];
    int index = 0;
    Sprite Sprite
    {
        get
        {
            index++;
            if (index > sprites.Length - 1)
                index = 0;

            return sprites[index];
        }
    }

    public void SpriteSwitch(Transform caller, Transform sender)
    {
        GetComponent<SpriteRenderer>().sprite = Sprite;
    }
}
