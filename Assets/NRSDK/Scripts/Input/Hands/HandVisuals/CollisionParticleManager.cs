using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticleManager : MonoBehaviour , ICreateCollisionEffect
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateEffect(Vector3 _pos)
    {
        transform.position = _pos;
        StartCoroutine(DestroyParticle());
    }

    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
