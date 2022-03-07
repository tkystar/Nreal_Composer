using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiManager : MonoBehaviour , ICreateCollisionEffect
{
    
    public void CreateEffect(Vector3 _pos)
    {
        this.transform.position = _pos;
        this.transform.rotation = Quaternion.Euler(0,-60,0);
        Rigidbody sushiRB = this.gameObject.GetComponent<Rigidbody>();
        if(sushiRB == null) return;
        sushiRB.AddForce(Vector3.up * 30);
        StartCoroutine(DestroySushi());

    }
    
    IEnumerator DestroySushi()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
