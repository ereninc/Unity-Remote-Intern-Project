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
        CheckNoobWood();
        CheckMidWood();
        CheckProWood();
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

    void CheckNoobWood()
    {
        if (AI.instance.WoodList() <= 0 && !AI.instance.isGrounded)
        {
            aiNoobAnimator.GetComponent<Animator>().enabled = false;
            aiNoobAnimator.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    void CheckMidWood()
    {
        if (AI.instance.WoodList() <= 0 && !AI.instance.isGrounded)
        {
            aiMidAnimator.GetComponent<Animator>().enabled = false;
            aiMidAnimator.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    void CheckProWood()
    {
        if (AI.instance.WoodList() <= 0 && !AI.instance.isGrounded)
        {
            aiProAnimator.GetComponent<Animator>().enabled = false;
            aiProAnimator.transform.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
