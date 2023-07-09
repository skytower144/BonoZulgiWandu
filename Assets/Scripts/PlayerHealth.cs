using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int maxHealth, currentHealth;

    [SerializeField] private List<GameObject> heartImageList;

    void Start()
    {
        currentHealth = maxHealth = heartImageList.Count;
    }

    public void DecreaseHealth(int amount = 1)
    {
        SetHeartUI();
        currentHealth -= amount;

        if (currentHealth <= 0) {
            currentHealth = 0;
            GameManager.instance.GameOver();
        }
    }

    private void SetHeartUI()
    {
        if (currentHealth <= 0) return;
        heartImageList[currentHealth - 1].SetActive(false);
    }

    public void ResetHealth()
    {
        foreach (GameObject heart in heartImageList) {
            heart.SetActive(true);
        }
        currentHealth = maxHealth = heartImageList.Count;
    }
}
