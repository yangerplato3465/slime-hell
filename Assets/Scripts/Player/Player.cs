using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player stats")]
    public float moveSpeed = 5f;
    public float health = 100;

    [Header("Other")]
    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    private bool isRolling = false;
    private PlayerStates playerState = PlayerStates.idle;

    public enum PlayerStates {
        idle,
        run,
        roll,
        death
    }

    void Update() {
        CheckInput();
        switch(playerState) {
            case PlayerStates.idle:
                // animator.SetBool("IsRolling", false);
                animator.SetFloat("Speed", 0);
                break;

            case PlayerStates.run:
                // animator.SetBool("IsRolling", false);
                animator.SetFloat("Speed", movement.sqrMagnitude);
                if(movement.x > 0) transform.localScale = new Vector3(1, 1, 1);
                else if(movement.x < 0) transform.localScale = new Vector3(-1, 1, 1);
                break;

            // case PlayerStates.roll:
            //     DodgeRoll();
            //     // if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerRoll")) {
            //     //     isRolling = false;
            //     // }
            //     break;

            case PlayerStates.death:
                break;
        }
    }

    private void CheckInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if(movement.x == 0 && movement.y == 0 && !isRolling) {
            playerState = PlayerStates.idle;
        }
        if((movement.x != 0 || movement.y != 0) && !isRolling) {
            playerState = PlayerStates.run;
        }
        // if (Input.GetButtonDown("Fire2") || Input.GetButtonDown("Space")) {
        //     playerState = PlayerStates.roll;
        // }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // private void DodgeRoll() {
    //     isRolling = true;
    //     animator.SetBool("IsRolling", true);
    // }
}
