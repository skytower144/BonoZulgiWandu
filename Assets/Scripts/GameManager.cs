using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Transform enemyParent;
    public PlayerPhysics playerPhysics;
    public PlayerBehaviour playerBehaviour;

    private List<GameObject> objects_ctrl = new List<GameObject>();

    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    void Start()
    {
        objects_ctrl.Add(playerPhysics.gameObject);
        foreach (Transform enemy in enemyParent) {
            objects_ctrl.Add(enemy.gameObject);
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
