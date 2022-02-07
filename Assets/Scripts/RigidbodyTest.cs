using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyTest : MonoBehaviour
{
    private Rigidbody rb;
    private float speed;
    private Vector3 latestPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.velocity.magnitude);
        
        speed = ((this.gameObject.transform.position -latestPos) / Time.deltaTime).magnitude;
        latestPos = this.gameObject.transform.position;

        Debug.Log(speed);

    }
}
