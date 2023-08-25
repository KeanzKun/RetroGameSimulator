using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BulletTutorial : MonoBehaviour
{
    
    public float bulletSpeed = 5.0f;
    
    void Start()
    {

    }

    void Update(){
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        //play SFX 
        //delete bullet when exiting a trigger
        Destroy(gameObject);
        Debug.Log("terrainExit");
    }
}
