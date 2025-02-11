using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSprite : MonoBehaviour
{
    PlayerController controller;
    SpriteRenderer sprite;
    Animator animator;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
        
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        
    }


    void Update()
    {
        // Handle flipping the sprite
        if (controller.moveDirection == PlayerController.MoveDirection.DOWNLEFT || controller.moveDirection == PlayerController.MoveDirection.LEFT || controller.moveDirection == PlayerController.MoveDirection.UPLEFT)
            sprite.flipX = true;
        
        else if (controller.moveDirection == PlayerController.MoveDirection.DOWNRIGHT || controller.moveDirection == PlayerController.MoveDirection.RIGHT || controller.moveDirection == PlayerController.MoveDirection.UPRIGHT)
            sprite.flipX = false;
        

        //Handle walk animations
        if (controller.moveDirection == PlayerController.MoveDirection.STATIC)
            animator.SetBool("Static", true);
                       
        else
            animator.SetBool("Static", false);
        
    }
}
