using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3.0f;
    [SerializeField] private string _targetTag = "Target";
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(_targetTag))
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
