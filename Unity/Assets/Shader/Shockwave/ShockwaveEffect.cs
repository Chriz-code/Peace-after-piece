using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveEffect : MonoBehaviour
{
    public Shader shader;

    public AnimationCurve defaultCurve;
    public AnimationCurve _AnnulusMin, _MaxRange, _DistortionStrength;
    public float speed = 1;

    private Material m_Material;
    Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(shader);
                m_Material.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_Material;
        }
    }

    public void ShockWaveEffect()
    {

    }
    public void ShockWaveEffect(AnimationCurve _Annulus, AnimationCurve _MaxRange, AnimationCurve _DistortionStrenght)
    {

    }

    public IEnumerator ApplyEffect(AnimationCurve _Annulus, AnimationCurve _MaxRange, AnimationCurve _DistortionStrenght)
    {
        if (_Annulus == null)
            _Annulus = defaultCurve;
        if (_MaxRange == null)
            _MaxRange = defaultCurve;
        if (_DistortionStrenght == null)
            _DistortionStrenght = defaultCurve;

        for (float i = 0; i < 1; i += Time.deltaTime * speed)
        {
            //"Annulus Radius"
            material.SetFloat("Annulus Radius", _Annulus.Evaluate(i));
            //"Outer Radius"
            material.SetFloat("Outer Radius", _MaxRange.Evaluate(i));
            //"Distortion Strength"
            material.SetFloat("Distortion Strength", _DistortionStrenght.Evaluate(i));

            yield return new WaitForSeconds(Time.deltaTime * speed);
        }
    }
}
