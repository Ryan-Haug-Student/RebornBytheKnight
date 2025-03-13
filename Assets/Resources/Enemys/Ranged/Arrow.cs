using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Entity
{
    void Start()
    {
        StartCoroutine("EndOfLife");
    }

    private IEnumerator EndOfLife()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == PlayerController.instance.gameObject)
            PlayerController.instance.health -= 8;

        Destroy(gameObject);
    }

}
