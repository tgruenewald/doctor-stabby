using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnHurt : MonoBehaviour
{
    [Header("Default Destruction Time")]
    public float timeToDestruction;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", timeToDestruction);
    }
    

    void DestroyObject(){
        Destroy(gameObject);
    }
}
