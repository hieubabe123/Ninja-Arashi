using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAndDisableObject : MonoBehaviour
{
    public List<GameObject> objectEnables = new List<GameObject>();
    public List<GameObject> objectDisables = new List<GameObject>();

    public void OnClickInButton()
    {
        foreach (GameObject gameObj in objectEnables)
        {
            gameObj.SetActive(true);
        }
        foreach (GameObject gameObj in objectDisables)
        {
            gameObj.SetActive(false);
        }
    }
}
