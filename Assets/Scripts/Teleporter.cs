using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public Sprite activated;
    public bool readyToTeleport;

    public SpriteRenderer sr;

    private void Update()
    {
        if (PlayerController.instance.stageOver && !readyToTeleport)
        { 
            readyToTeleport = true; 
            sr.sprite = activated; 
        }

        if (readyToTeleport)
            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 2)
                Teleport();
    }

    private void Teleport()
    {
        //set variables
        PlayerController.instance.stageOver = false;
        readyToTeleport = false;
        PlayerController.instance.stage += 1;

        //reset player position to center
        PlayerController.instance.transform.position = Vector3.zero;

        //decide if player needs to go to boss or regular stage and go to there
        if (PlayerController.instance.stage % 5 != 0)
        {
            int rnd = Random.Range(1, 6);
            SceneManager.LoadScene("Level " + rnd);
        }
        else
        {
            //random feature is currently pointless due to only 1 boss
            int rnd = Random.Range(1, 2);
            SceneManager.LoadScene("Boss " + rnd);
        }
    }
}
