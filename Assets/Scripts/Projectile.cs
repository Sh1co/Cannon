using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3.0f;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Target"))
        {
            Destroy(other.collider.gameObject);
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private float timeCounter = 0;
}
