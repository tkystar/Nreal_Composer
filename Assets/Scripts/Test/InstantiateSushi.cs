namespace test
{


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class InstantiateSushi : MonoBehaviour
    {
        private GameObject[] sushi;

        // Start is called before the first frame update
        void Start()
        {
            sushi = Resources.LoadAll<GameObject>("Sushi");
            foreach (var item in sushi)
            {
                print(item);
                Instantiate(item);
            }
            

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}