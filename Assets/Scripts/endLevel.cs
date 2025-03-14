using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel : MonoBehaviour
{
    private void OnDestroy()
    {
        PlayerController.instance.stageOver = true;
    }
}
