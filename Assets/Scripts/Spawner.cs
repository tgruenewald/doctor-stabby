using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timer;
    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating( "Summon", 5.0f, timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Summon()
    {
        Instantiate(target, transform);
    }
}
