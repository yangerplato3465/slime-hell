using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player stats")]
    public float moveSpeed = 5f;
    public float rollSpeed = 20f;
    public float rollTime = 0.4f;
    public float rollCoolDown = 0.2f;
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
        // Debug.Log("player: " + playerState);
        switch(playerState) {
            case PlayerStates.idle:
                animator.SetFloat("Speed", 0);
                break;

            case PlayerStates.run:
                animator.SetFloat("Speed", movement.sqrMagnitude);
                if(movement.x > 0) transform.localScale = new Vector3(1, 1, 1);
                else if(movement.x < 0) transform.localScale = new Vector3(-1, 1, 1);
                break;

            case PlayerStates.roll:
                StartCoroutine(DodgeRoll());
                break;

            case PlayerStates.death:
                break;
        }
    }

    private void FixedUpdate() {
        switch(playerState) {
            case PlayerStates.idle:
                rb.velocity = movement * moveSpeed;
                break;

            case PlayerStates.run:
                rb.velocity = movement * moveSpeed;
                break;
                
            case PlayerStates.roll:
                break;

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
        if ((Input.GetButtonDown("Fire2") || Input.GetButtonDown("Space")) && !isRolling && movement != new Vector2(0, 0)) {
            playerState = PlayerStates.roll;
        }
    }


    private IEnumerator DodgeRoll() {
        if (isRolling) yield break;
        isRolling = true;
        animator.SetBool("IsRolling", true);
        
        rb.velocity = movement * rollSpeed;
        yield return new WaitForSeconds(rollTime);
        animator.SetBool("IsRolling", false);
        yield return new WaitForSeconds(rollCoolDown);
        isRolling = false;
    }
}
