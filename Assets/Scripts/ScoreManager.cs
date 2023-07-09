using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText, comboText;
    [SerializeField] private int currentScore;
    private int tempComboScore = 0;

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
    }

    public void UpdateComboScore(EnemyType enemy_type, int combo)
    {
        switch (enemy_type) {
            case EnemyType.Knight:
                tempComboScore += 10;
                currentScore += 10;
                break;

            default:
                break;
        }
        SetCurrentScoreText();
        SetCurrentComboText(combo);
    }
    

    public void CalculateFinalComboScore(int combo)
    {
        currentScore += (tempComboScore * combo) - tempComboScore;
        tempComboScore = 0;

        SetCurrentScoreText();
        SetCurrentComboText(0);
    }

    private void SetCurrentScoreText()
    {
        scoreText.text = $"{currentScore}";
    }

    private void SetCurrentComboText(int combo)
    {
        comboText.text = $"x {combo}";
        if (combo > 0) comboText.color = Color.yellow;
        else comboText.color = Color.white;
    }

}
