using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        SpriteHandling();
    }

    private void SpriteHandling()
    {
        if (rb.velocity.x > 0)
            sr.flipX = false;
        else if (rb.velocity.x < 0)
            sr.flipX = true;
    }

}
