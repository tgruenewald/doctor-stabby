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
        InvokeRepeating("DecreaseSpawnInterval", 10f, 15f);
        InvokeRepeating("Summon", initTimer, timer);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Summon()
    {
        Instantiate(target, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), gameObject.transform.rotation);
    }

    void DecreaseSpawnInterval()
    {
        if (timer > 0.5)
        {
            print("Creating zombies every " +  timer);
            timer -= 0.25f;
            CancelInvoke("Summon");
            InvokeRepeating("Summon", timer, timer);
        }

    }
}
