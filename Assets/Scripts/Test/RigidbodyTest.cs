using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyTest : MonoBehaviour
{
    private Rigidbody _rb;
    private float _speed;
    private Vector3 _latestPos;
    // Start is called before the first frame update
    void Start()
    {
        _rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.velocity.magnitude);
        
        _speed = ((this.gameObject.transform.position - _latestPos) / Time.deltaTime).magnitude;
        _latestPos = this.gameObject.transform.position;


    }
}
