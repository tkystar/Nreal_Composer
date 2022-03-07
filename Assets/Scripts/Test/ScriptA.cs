namespace test
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class ScriptA : MonoBehaviour
    {
        //[SerializeField] private InstantiateSushi insntantiatesushi;
        // Start is called before the first frame update
        void Start()
        {
            this.gameObject.AddComponent<InstantiateSushi>();
        }
    
        // Update is called once per frame
        void Update()
        {
            
        }
    }

}

