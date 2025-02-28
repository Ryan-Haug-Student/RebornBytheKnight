using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public PlayerController PC { get; private set; }

    [Header("Movement")]
    public int moveSpeed = 3;
    public float dashStrength = 4;
    public float dashCooldown = 2;
    public bool canDash = true;
    private bool isDashing;

    void Start()
    {
        //ensure only one player persists accross scenes
        if (PC == null)
            PC = this;
        else
            Destroy(gameObject);
    }

    protected override void Update()
    {
        //run parent entity update function
        base.Update(); 

        Move();
    }

    private void Move()
    {
        Vector2 direction;
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //direction *= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine("Dash");

        if (!isDashing)
            rb.velocity = direction * moveSpeed;
        else
            rb.velocity = direction * moveSpeed * dashStrength;

        direction = Vector2.zero;
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        yield return new WaitForSeconds(.15f);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
