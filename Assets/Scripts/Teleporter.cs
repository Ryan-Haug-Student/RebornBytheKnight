using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(PlayerController.instance.transform.position, transform.position) < 1 && PlayerController.instance.stageOver)
        {
            int rnd = Random.Range(1, 6);
            PlayerController.instance.stageOver = false;
            PlayerController.instance.stage += 1;

            PlayerController.instance.transform.position = Vector3.zero;
            SceneManager.LoadScene("Level " + rnd);
        }
    }
}
