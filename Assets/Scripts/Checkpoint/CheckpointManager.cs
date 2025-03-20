using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public Checkpoint currentCheckpoint;
    public List<Checkpoint> checkpoint = new List<Checkpoint>();
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (currentCheckpoint == null)
        {
            currentCheckpoint = checkpoint[0];
        }
    }

    public Vector2 CheckpointPosition()
    {
        return currentCheckpoint.gameObject.transform.position;
    }

    public void RespawnPlayer(GameObject player, GameObject deadPlayer, float delayTime)
    {
        Debug.Log("Waiting");
        StartCoroutine(RespawnCoroutine(player, deadPlayer, delayTime));
    }

    private IEnumerator RespawnCoroutine(GameObject player, GameObject deadPlayer, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Waiting Respawn");
        player.transform.position = CheckpointPosition();
        player.SetActive(true);
        deadPlayer.SetActive(false);
    }



}
