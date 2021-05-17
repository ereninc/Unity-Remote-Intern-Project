using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject aiNoobAnimator;
    [SerializeField] private GameObject aiMidAnimator;
    [SerializeField] private GameObject aiProAnimator;

    private void Start()
    {
        StopAnimatons();
    }

    void Update()
    {
        StartAnimations();
    }

    void StopAnimatons()
    {
        aiNoobAnimator.GetComponent<Animator>().enabled = false;
        aiMidAnimator.GetComponent<Animator>().enabled = false;
        aiProAnimator.GetComponent<Animator>().enabled = false;
    }

    void StartAnimations()
    {
        if (PlayerController.instance.isStarted)
        {
            aiNoobAnimator.GetComponent<Animator>().enabled = true;
            aiMidAnimator.GetComponent<Animator>().enabled = true;
            aiProAnimator.GetComponent<Animator>().enabled = true;
        }
    }
}
