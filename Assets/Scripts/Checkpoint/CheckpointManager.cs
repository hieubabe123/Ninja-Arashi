using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public Checkpoint currentCheckpoint;
    public ParticleSystem revivalPlayerEffect;
    public List<Checkpoint> checkpoint = new List<Checkpoint>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
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
        StartCoroutine(RespawnCoroutine(player, deadPlayer, delayTime));
    }

    private IEnumerator RespawnCoroutine(GameObject player, GameObject deadPlayer, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        player.transform.position = CheckpointPosition();
        player.SetActive(true);
        deadPlayer.SetActive(false);

        Vector3 revivalEffectTransform = new Vector3(player.transform.position.x, player.transform.position.y - 2f, player.transform.position.z);

        Instantiate(revivalPlayerEffect, revivalEffectTransform, Quaternion.identity);
        AudioManager.instance.PlayListSFX(AudioManager.instance.playerRevivalSFX);
    }



}
