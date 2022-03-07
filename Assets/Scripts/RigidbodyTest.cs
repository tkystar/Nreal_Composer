using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyTest : MonoBehaviour
{
    public GameObject sphere;
    public float force;
    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InstantiateSphere();
        }
    }

    void InstantiateSphere()
    {
        GameObject a = Instantiate(sphere,new Vector3(0,2,2),Quaternion.identity);
        Rigidbody rb = a.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,1,0) * force);
        Destroy(a,destroyTime);
    }
}
