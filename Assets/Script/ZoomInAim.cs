using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInAim : MonoBehaviour
{
    [SerializeField] private int zoom = 20;
    [SerializeField] private int normal = 60;
    [SerializeField] private float smooth = 5;
   // [SerializeField] private GunTutorial GunTutorial;
    private bool isZoomed = false;
    // private bool notZoomed = true;

    void Update()
    {

        //bool isReloading = GunTutorial.IsReloading();

       // if (!isReloading)
       // {
            if (Input.GetMouseButtonDown(1))
            {
                isZoomed = true;
            }

            if (isZoomed)
            {
                GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
            }

            if (Input.GetMouseButtonUp(1))
            {
                isZoomed = false;
            }

            if (!isZoomed)
            {
                GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
            }
      //  }
    }

    public bool IsZooming()
    {
        return isZoomed;
    }
}



