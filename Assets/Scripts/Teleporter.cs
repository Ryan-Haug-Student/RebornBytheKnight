using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(PlayerController.instance.transform.position, transform.position) < 1 && PlayerController.instance.stageOver)
        {
            PlayerController.instance.stageOver = false;
            PlayerController.instance.stage += 1;

            PlayerController.instance.transform.position = Vector3.zero;

            if (PlayerController.instance.stage % 5 != 0)
            {
                int rnd = Random.Range(1, 6);
                SceneManager.LoadScene("Level " + rnd);
            }
            else
            {
                int rnd = Random.Range(1, 3);
                SceneManager.LoadScene("Boss " + rnd);
            }
        }
    }
}
