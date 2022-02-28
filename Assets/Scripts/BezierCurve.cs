using System;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private Transform _point3;
    [SerializeField] private float _loopTime = 10f;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_doingCurve) return;
        
        _rb.MovePosition(GetPosition(GetT()));
        if (_timeCounter >= _loopTime)
        {
            _timeCounter = 0;
        }

        _timeCounter += Time.deltaTime;
    }


    private Vector3 GetPosition(float t)
    {
        var position = (((1 - t) * (1 - t)) * _point1.position) + (2 * (1 - t) * t * _point2.position) + (t * t * _point3.position);
        return position;
    }

    private Vector3 GetVelocity(float t)
    {
        var velocity = (-2 * (1 - t) * _point1.position) + (2 * (1 - 2 * t) * _point2.position) + (2 * t * _point3.position);
        return velocity;
    }

    private float GetT()
    {
        var t = 1 - Mathf.Abs((_timeCounter / _loopTime) * 2 - 1);
        return t;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_doingCurve) return;
        Debug.Log(other.gameObject.name);
        _doingCurve = false;
        _rb.isKinematic = false;
        _rb.velocity = GetVelocity(GetT());
    }


    private float _timeCounter;
    private bool _doingCurve = true;
    private Rigidbody _rb;
}
