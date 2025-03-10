using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float lifetime = 1f;
    private float timeAlive = 0f;

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifetime)
            Destroy(gameObject);
    }
}