using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MinimapImage : MonoBehaviour
{
    [Header("----------Sprites---------")]
    public string mapName;
    public Sprite unlockedMap;
    public Sprite lockedMap;
    public Sprite selectedMap;

    public bool isSelected;
    public bool isUnlocked = true;
    private Image image;

    public static MinimapImage currentSelectedMap;


    void Awake()
    {
        image = GetComponent<Image>();
    }


    void Start()
    {
        isSelected = false;
        if (isUnlocked)
        {
            image.sprite = unlockedMap;
        }
        else
        {
            image.sprite = lockedMap;
        }
    }


    public void OnClickSelectMap()
    {
        if (!isUnlocked)
        {
            return;
        }

        if (currentSelectedMap != null && currentSelectedMap != this)
        {
            currentSelectedMap.UnSelect();
        }

        if (!isSelected)
        {
            Select();
        }
        else
        {
            FindObjectOfType<SceneController>().SceneChange(mapName);
        }
    }

    private void Select()
    {
        image.sprite = selectedMap;
        isSelected = true;
        currentSelectedMap = this;
    }

    public void UnSelect()
    {
        isSelected = false;
        image.sprite = unlockedMap;
    }

}
