using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.SetBool("isStarted", false);
        animator.SetBool("haveWood", false);
        animator.SetBool("isFinished", false);
    }

    private void Update()
    {
        GameStarted();
        HaveWood();
        GameFinished();
    }

    private void GameStarted()
    {
        if (PlayerController.instance.isStarted)
        {
            animator.SetBool("isStarted", true);
        }
    }

    private void HaveWood()
    {
        if (CollisionManager.instance.WoodCount() > 0)
        {
            animator.SetBool("haveWood", true);
        }
        else
        {
            animator.SetBool("haveWood", false);
        }
    }

    private void GameFinished()
    {
        if (CollisionManager.instance.isFinished)
        {
            animator.SetBool("isFinished", true);
        }
    }
}
