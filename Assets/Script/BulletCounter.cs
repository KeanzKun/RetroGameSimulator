using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    [SerializeField] private GunTutorial gunTutorial;

    public Text ammoDisplay;
    
    void Awake()
    {
        int bulletLeft = gunTutorial.BulletCount();
        ammoDisplay.text = bulletLeft + " / 5";
    }
    void Update()
    {
         ammoDisplay.text = gunTutorial.BulletCount().ToString() + " / 5";
    }






}
