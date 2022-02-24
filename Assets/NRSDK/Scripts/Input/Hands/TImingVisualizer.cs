using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImingVisualizer : MonoBehaviour
{
    public GameObject hitPointObj;
    public GameObject spawnPointObj;
    private Vector3 _hitPoint;
    private Vector3 _spawnPoint;
       
    void Start()
    {
        _hitPoint = hitPointObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
