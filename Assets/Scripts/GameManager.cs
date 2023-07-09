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

    private List<GameObject> objects_ctrl = new List<GameObject>();

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

}
