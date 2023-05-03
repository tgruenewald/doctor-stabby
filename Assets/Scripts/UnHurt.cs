using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnHurt : MonoBehaviour
{
    bool isHitting = false;
    [Header("Default Destruction Time")]
    public float timeToDestruction;
    // Start is called before the first frame update
    void Start()
    {
        isHitting = false;
        Invoke("DestroyObject", timeToDestruction);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHitting && collision.gameObject.CompareTag("Zombie"))
        {
            isHitting = true;
            collision.gameObject.GetComponent<basicZombie>().ZombieHit();
            Destroy(gameObject);
        }
    }

            void DestroyObject(){
        Destroy(gameObject);
    }
}
