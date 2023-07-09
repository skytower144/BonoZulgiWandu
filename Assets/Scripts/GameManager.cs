using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public PlayerPhysics playerPhysics;
    public PlayerBehaviour playerBehaviour;
    public PlayerHealth playerHealth;
    public EnemySpawner enemySpawner;
    public GameTimer gameTimer;
    public ScoreManager scoreManager;

    [SerializeField] private GameObject gameOverUI;

    private List<GameObject> objects_ctrl = new List<GameObject>();
    [System.NonSerialized] public bool isGameOver = false;

    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public void SetEnemyObjectControl()
    {
        objects_ctrl.Clear();

        objects_ctrl.Add(playerPhysics.gameObject);
        foreach (GameObject enemy in enemySpawner.current_enemies) {
            objects_ctrl.Add(enemy);
        }
    }

    public void StopAllObjects()
    {
        foreach (GameObject ctrl in objects_ctrl) {
            if (ctrl)
                ctrl.GetComponent<ObjectControl>().StopObject();
        }
    }

    public void PlayAllObjects()
    {
        foreach (GameObject ctrl in objects_ctrl) {
            if (ctrl)
                ctrl.GetComponent<ObjectControl>().PlayObject();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over.");
        isGameOver = true;
        playerBehaviour.EraseBullet();
        gameTimer.StopTimer();
        gameOverUI.SetActive(true);
    }

    public void RetryGame()
    {
        isGameOver = false;

        playerPhysics.transform.localPosition = new Vector2(0f, 0f);
        playerPhysics.playerRb.velocity = Vector3.zero;
        playerPhysics.Launch();

        playerHealth.ResetHealth();
        scoreManager.ResetScore();
        
        enemySpawner.ClearAllEnemy();
        gameOverUI.SetActive(false);
    }

}
