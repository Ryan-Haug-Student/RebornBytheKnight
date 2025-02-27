using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public PlayerController PC { get; private set; }

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
        base.Update();
    }
}
