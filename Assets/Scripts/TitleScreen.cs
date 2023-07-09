using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private SounEffectsPlayer soundEffects;

    public void StartGame()
    {
        soundEffects.PlaySound(0);
        Invoke("LoadGame", 0.5f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
