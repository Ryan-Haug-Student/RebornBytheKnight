using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private GameObject P1; //going vertical
    [SerializeField] private GameObject P2;

    [SerializeField] private GameObject P3; //going sideways
    [SerializeField] private GameObject P4;

    bool goingOut = true;

    // Update is called once per frame
    void Update()
    {
        if (goingOut)
        {
            P1.transform.position = Vector3.Lerp(P1.transform.position, PC.Instance.transform.position + (Vector3.up * 2), 2 * Time.deltaTime);
            P2.transform.position = Vector3.Lerp(P2.transform.position, PC.Instance.transform.position + (Vector3.down * 2), 2 * Time.deltaTime);

            P3.transform.position = Vector3.Lerp(P3.transform.position, PC.Instance.transform.position + (Vector3.right * 2), 2 * Time.deltaTime);
            P4.transform.position = Vector3.Lerp(P4.transform.position, PC.Instance.transform.position + (Vector3.left * 2), 2 * Time.deltaTime);

            if (Vector3.Distance(P1.transform.position, PC.Instance.transform.position + (Vector3.up * 2)) < 0.1f)
                goingOut = false;
        }
        else
        {
            P1.transform.position = Vector3.Lerp(P1.transform.position, PC.Instance.transform.position, 2 * Time.deltaTime);
            P2.transform.position = Vector3.Lerp(P2.transform.position, PC.Instance.transform.position, 2 * Time.deltaTime);

            P3.transform.position = Vector3.Lerp(P3.transform.position, PC.Instance.transform.position, 2 * Time.deltaTime);
            P4.transform.position = Vector3.Lerp(P4.transform.position, PC.Instance.transform.position, 2 * Time.deltaTime);

            if (Vector3.Distance(P1.transform.position, PC.Instance.transform.position) < 0.01f)
                goingOut = true;
        }

    }
}
