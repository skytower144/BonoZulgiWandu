using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour
{
    [SerializeField] private float duration;

    void OnEnable()
    {
        Destroy(gameObject, duration);
    }
}
