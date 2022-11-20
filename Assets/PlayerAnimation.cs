using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerController.OnPlayerJump += PlayerController_OnPlayerJump;
        playerController.OnPlayerUpdateVelocity += PlayerController_OnPlayerUpdateVelocity;
        playerController.OnPlayerUpdateGrounded += PlayerController_OnPlayerUpdateGrounded;
    }

    private void OnDestroy()
    {
        playerController.OnPlayerJump -= PlayerController_OnPlayerJump;
        playerController.OnPlayerUpdateVelocity -= PlayerController_OnPlayerUpdateVelocity;
        playerController.OnPlayerUpdateGrounded -= PlayerController_OnPlayerUpdateGrounded;
    }

    private void PlayerController_OnPlayerJump(object sender, EventArgs e)
    {
        animator.SetTrigger("jump");
    }

    private void PlayerController_OnPlayerUpdateVelocity(object sender, PlayerController.UpdateVelocityArgs e)
    {
        animator.SetBool("movingH", e.velocityX != 0);
    }

    private void PlayerController_OnPlayerUpdateGrounded(object sender, PlayerController.UpdateGroundedArgs e)
    {
        animator.SetBool("onGround", e.onGround);
    }
}
