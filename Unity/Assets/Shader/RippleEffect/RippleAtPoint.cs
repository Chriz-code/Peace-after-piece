using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleAtPoint : MonoBehaviour
{
    public float fadeSpeed = 1;
    public void Ripple(Transform pos)
    {
        if (!Camera.main.GetComponent<RippleEffect>())
            return;
        RippleEffect rippleEffect = Camera.main.GetComponent<RippleEffect>();

        Vector3 itemPos = Camera.main.WorldToScreenPoint(pos.position);
        rippleEffect.Emit(itemPos);
    }
    public float ReplaceTransparency(SpriteRenderer renderer, UnityEngine.UI.Image uiRenderer, bool reverse = false)
    {
        Color rendererColor = renderer.color;
        Color uiColor = uiRenderer.color;
        rendererColor.a = reverse ? rendererColor.a + Time.deltaTime * fadeSpeed : rendererColor.a - Time.deltaTime * fadeSpeed;
        uiColor.a = 1 - rendererColor.a;
        uiRenderer.color = uiColor;
        renderer.color = rendererColor;
        return rendererColor.a;
    }
}
