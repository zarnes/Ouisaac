using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ouisaac
{
    public class SeedChanger : MonoBehaviour
    {
        public bool Verbose = false;
        public int Increment = 1;
        public float IncrementTime = .5f;
        private float nextIncrement;

        private GraphGenerator gg;
    
        // Start is called before the first frame update
        void Start()
        {
            gg = GetComponent<GraphGenerator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time > nextIncrement)
            {
                nextIncrement += IncrementTime;
                gg.Seed += Increment;
                if (Verbose)
                    Debug.Log("Genereating seed " + gg.Seed);
            }
        }
    }
}
