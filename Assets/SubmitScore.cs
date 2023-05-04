using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.Networking;

public class SubmitScore : MonoBehaviour
{
    void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });
    }
    IEnumerator CleanUpPlayerName(string name)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://www.purgomalum.com/service/plain?text=" + name))
        {
            yield return webRequest.SendWebRequest();
            string userName = "dr stabby";
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error while sending request: " + webRequest.error);
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                userName = webRequest.downloadHandler.text;
            }

            userName = userName.Substring(0, System.Math.Min(userName.Length, 15));
            LootLockerSDKManager.SetPlayerName(userName, (response) =>
            {
                GameObject scoreStatusObj = GameObject.Find("ScoreStatus");
                Text scoreStatus = scoreStatusObj.GetComponent<Text>();

                GameObject scoreTextObj = GameObject.Find("Score");
                Text scoreText = scoreTextObj.GetComponent<Text>();

                if (response.success)
                {
                    string leaderboardKey = "drstabby-leaderboard-key";
                    LootLockerSDKManager.SubmitScore(userName, int.Parse(scoreText.text), leaderboardKey, (response) =>
                    {

                        if (response.statusCode == 200)
                        {
                            Debug.Log("Successful");
                            scoreStatus.text = "Score Successfully Sent";
                        }
                        else
                        {
                            scoreStatus.text = "Problem sending score";
                            Debug.Log("failed: " + response.Error);
                        }
                    });
                }
                else
                {
                    scoreStatus.text = "Problem sending score";
                    
                }
            });

        }
    }

    public void LeaderboardSubmitScore()
    {
        GameObject inputFieldGO = GameObject.Find("PlayerName");
        InputField playerNameInputField = inputFieldGO.GetComponent<InputField>();
        print("Player: " + playerNameInputField.text);



        StartCoroutine(CleanUpPlayerName(playerNameInputField.text));

    }
}


