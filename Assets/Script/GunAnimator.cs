using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    private const string IS_ZOOMING = "IsZooming";
    private const string IS_RELOADING = "IsReloading";

    [SerializeField] private ZoomInAim zoomInAim;
    [SerializeField] private GunTutorial GunTutorial;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_ZOOMING, zoomInAim.IsZooming());
        animator.SetBool(IS_RELOADING, GunTutorial.IsReloading());
    }

}
