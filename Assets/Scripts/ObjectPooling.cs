using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    public GameObject player;
    private PlayerScriptableObject playerData;
    public int poolSize = 10;

    public List<GameObject> poolPrefabList = new List<GameObject>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerData = FindObjectOfType<PlayerMovement>().playerData;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(playerData.ShurikenPrefab);
            obj.SetActive(false);
            poolPrefabList.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < poolPrefabList.Count; i++)
        {
            if (!poolPrefabList[i].activeInHierarchy)
            {
                return poolPrefabList[i];
            }
        }
        return null;
    }










}
