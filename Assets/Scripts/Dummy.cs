using System;
using UnityEngine;


public class Dummy : MonoBehaviour
{
    [SerializeField] private string _projectileTag = "Projectile";
    [SerializeField] private int _ignoreRayCastLayer = 2;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_projectileTag)) return;
        Destroy(GetComponent<Animator>());
        GetComponent<Rigidbody>().isKinematic = false;
        gameObject.layer = _ignoreRayCastLayer;
        SetLayerRecursively(gameObject, _ignoreRayCastLayer);
    }
    
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }
       
        obj.layer = newLayer;
       
        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
