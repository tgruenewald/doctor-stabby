using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timer;
    public float initTimer;
    public GameObject target;
    public bool difficulty = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if(difficulty == false)
        {
            InvokeRepeating("Summon", initTimer, timer);
        }
        else
        {
            InvokeRepeating("Summon", initTimer, (timer/(Time.time + 1)));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Summon()
    {
        Instantiate(target, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), gameObject.transform.rotation);
    }
}
