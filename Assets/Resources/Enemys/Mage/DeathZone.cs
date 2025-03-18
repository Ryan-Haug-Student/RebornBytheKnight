using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public int DPT = 1; //damage per tick
    public float tr = 0.6f; //tick rate

    public float lifeSpan = 5;

    public bool canAttack = true;

    public ParticleSystem ps;

    private void Start()
    {
        Invoke("Die", lifeSpan);
        ps.Play();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.instance.gameObject)
        {
            canAttack = false;
            PlayerController.instance.health -= DPT;
            Invoke("ResetAttack", tr);
        }
    }
    void ResetAttack()
    {
        canAttack=true;
    }
}
