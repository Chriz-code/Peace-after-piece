using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ScoreCounter", menuName = "Zombie Survival/Score Counter", order = 2)]
[CreateAssetMenu(fileName = "DialogProfile", menuName = "Dialog/Profile", order = 0)]
public class DialogProfile : ScriptableObject
{
    public Sprite profileImage = null;
    public Color color = Color.white;
    public AudioClip[] textSounds = null;
}
