using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float _fovAngle = 80f;
    [SerializeField] private float _shootCooldown = 1.0f;
    [SerializeField] private Vector3 _boxCastSize = new Vector3(15f, 15f, 0);
    [SerializeField] private LayerMask _targetLayers;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private float _rotationSpeed = 1.5f;
    [SerializeField] private Transform _cannonBarrel;
    [SerializeField] private GameObject _projectileGameObject;
    [SerializeField] private float _projectileSpeed = 10f;

    private void Start()
    {
        _transform = transform;
        _originalRotation = _transform.rotation;
    }

    private void Update()
    {
        if(_target == null)
        {
            var step = _rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _originalRotation, step);
            
            var hits = Physics.BoxCastAll(transform.position, _boxCastSize, transform.forward, Quaternion.identity,
                float.MaxValue, _targetLayers);

            foreach (var hit in hits)
            {
                if (GetAngle(hit.transform) * 2 < _fovAngle)
                {
                    _target = hit.collider.gameObject;
                    break;
                }
            }
        }
        else
        {
            var desiredDirection = _target.transform.position - _transform.position;
            var cross = Vector3.Cross(desiredDirection, _transform.forward);
            var yDir = Mathf.Abs(cross.y) < 0.1 ? 0 : cross.y > 0 ? -1 : 1;
            transform.Rotate(0f, _rotationSpeed * Time.deltaTime * yDir, 0f);
            if (Mathf.Abs(cross.y) < 0.1 && _timeCounter >= _shootCooldown)
            {
                Shoot();
            }
        }

        if (_timeCounter < _shootCooldown)
        {
            _timeCounter += Time.deltaTime;
        }
    }

    private float GetAngle(Transform other)
    {
        var otherVector = other.position - _transform.position;
        return Vector3.Angle(_transform.forward, otherVector);
    }

    private void Shoot()
    {
        var projectile = Instantiate(_projectileGameObject, _projectileSpawnPoint.position, Quaternion.identity);
        var direction = _target.transform.position - projectile.transform.position;
        direction = direction.normalized;
        projectile.GetComponent<Rigidbody>().velocity = direction * _projectileSpeed;
        _timeCounter = 0;
    }
    
    


    private GameObject _target;
    private Transform _transform;
    private float _timeCounter = 0;
    private Quaternion _originalRotation;

}
