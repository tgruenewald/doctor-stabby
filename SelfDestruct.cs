using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;
 
     void Start ()
     {
         Destroy (gameObject, lifetime);
     }

}
