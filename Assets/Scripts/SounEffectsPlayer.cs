using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SounEffectsPlayer : MonoBehaviour
{
    public AudioSource src;
    public List<AudioClip> sfxList;

    public void PlaySound(int index)
    {
        src.clip = sfxList[index];
        src.Play();
    }

}
