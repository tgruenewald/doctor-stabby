using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    string leaderboardKey = "drstabby-leaderboard-key";
    public Text[] entries;

    void Start()
    {
        int maxScore = entries.Length;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
            Debug.Log("Getting scores");
            LootLockerSDKManager.GetScoreList(leaderboardKey, 20, 0, (response) =>
            {
                if (response.success)
                {
                    Debug.Log("get scores successful");
                    LootLockerLeaderboardMember[] scores = response.items;
                    for (int i = 0; i < scores.Length; i++)
                    {
                        LootLockerLeaderboardMember score = scores[i];
                        entries[i].text = score.rank + ".   " + score.player.name + "   -   " + score.score;

                        Debug.Log("rank: " + score.rank + " , " + score.player.name + ", " + score.score);
                    }
                    
                    if (scores.Length < maxScore)
                    {
                        for (int i = scores.Length; i < maxScore; i++)
                        {
                            entries[i].text = (i + 1).ToString() + ".   " + "none";
                        }
                    }
                    

                }
                else
                {
                    Debug.Log("Error: " + response.statusCode);
                }
            });
        });

    }
}
