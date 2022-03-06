using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
public class NootsManager : MonoBehaviour
{
    public float deadLine;

    public bool isMistake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        if (!isMistake)
        {
            if (this.transform.position.x > deadLine)
            {
                isMistake = true;
            }
        }
        
    }
}
