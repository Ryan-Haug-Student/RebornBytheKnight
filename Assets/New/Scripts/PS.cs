using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Update()
    {
        if (PC.Instance.direction != PC.MoveDirection.STATIC)
            animator.SetBool("Static", false);
        else
            animator.SetBool("Static", true);
    
        if (PC.Instance.direction == PC.MoveDirection.UPLEFT || PC.Instance.direction == PC.MoveDirection.LEFT || PC.Instance.direction == PC.MoveDirection.DOWNLEFT)
            spriteRenderer.flipX = true;
        else if (PC.Instance.direction == PC.MoveDirection.UPRIGHT || PC.Instance.direction == PC.MoveDirection.RIGHT || PC.Instance.direction == PC.MoveDirection.DOWNRIGHT)
            spriteRenderer.flipX = false;
    }
}
