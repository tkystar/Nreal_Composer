using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderExpansion : MonoBehaviour
{
    private GameObject _cylinder;

    private CapsuleCollider _cylinderCollider;
    // Start is called before the first frame update
    void Start()
    {
        _cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        _cylinderCollider = _cylinder.GetComponent<CapsuleCollider>();
        _cylinderCollider.height *= 2;
        _cylinderCollider.radius *= 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
