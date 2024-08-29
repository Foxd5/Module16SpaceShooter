using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f); //destroys the bullet after 5 seconds (offscreen)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
