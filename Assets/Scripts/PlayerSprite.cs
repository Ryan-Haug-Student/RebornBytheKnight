using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    PlayerController controller;
    SpriteRenderer sprite;
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
        
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (controller.moveDirection == PlayerController.MoveDirection.DOWNLEFT || controller.moveDirection == PlayerController.MoveDirection.LEFT || controller.moveDirection == PlayerController.MoveDirection.UPLEFT)
        {
            sprite.flipX = true;
        }
        else if (controller.moveDirection == PlayerController.MoveDirection.DOWNRIGHT || controller.moveDirection == PlayerController.MoveDirection.RIGHT || controller.moveDirection == PlayerController.MoveDirection.UPRIGHT)
        {
            sprite.flipX = false;
        }
    }
}
