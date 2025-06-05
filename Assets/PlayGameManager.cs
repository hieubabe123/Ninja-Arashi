using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class PlayGameManager : MonoBehaviour
{
    public TextMeshProUGUI detailText;
    // Start is called before the first frame update
    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

            detailText.text = "SUCCESS" + name;
        }
        else
        {
            detailText.text = "Sign in Failed!";
        }
    }
}
