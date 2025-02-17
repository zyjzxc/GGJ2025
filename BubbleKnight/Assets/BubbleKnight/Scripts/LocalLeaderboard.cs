using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LocalLeaderboard : MonoBehaviour
{
    public static LocalLeaderboard instance;
    public string[] leaderboardTexts = new string[10]; // 用于显示排行榜的 UI 文本元素

    bool rankShow = false;

    int hasRank = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        //ShowLeaderboard();

        for (int i = 0; i < 10; i++)
        {
            leaderboardTexts[i] = "0";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.DeleteAll();
        }
        //else if (Input.GetKeyDown(KeyCode.U))
        //{
        //    SaveScore(10000);
        //}
    }

    public void ShowRank()
    {
        //ShowLeaderboard();
        UIManager.instance.rankUI.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = ((int)GameManager._instance.nowHeight).ToString();



        if(PlayerPrefs.HasKey("BastTime"))
        {
            float seconds = PlayerPrefs.GetFloat("BastTime");
            int minutes = Mathf.FloorToInt(seconds / 60);
            int secs = Mathf.FloorToInt(seconds % 60);
            string str = string.Format("{0:00}:{1:00}", minutes, secs);
            UIManager.instance.rankUI.transform.Find("BaseTime").GetComponent<TextMeshProUGUI>().text = str;
        } else
        {
            UIManager.instance.rankUI.transform.Find("BaseTime").GetComponent<TextMeshProUGUI>().text = "NoBody";
        }

        
        //for (int i = 0; i < leaderboardTexts.Length; i++)
        //{
        //    int index = 9 - i;
        //    if(i < hasRank)
        //    {
        //        float seconds = float.Parse(leaderboardTexts[i]);
        //        int minutes = Mathf.FloorToInt(seconds / 60);
        //        int secs = Mathf.FloorToInt(seconds % 60);
        //        string str = string.Format("{0:00}:{1:00}", minutes, secs);

        //        UIManager.instance.rankUI.transform.GetChild(index).GetComponent<TextMeshProUGUI>().text = "Rank " + (i + 1) + ": " + str;
        //    }

        //}
    }

    public void SaveBestTime(float score)
    {
        if (!PlayerPrefs.HasKey("BastTime"))
        {
            PlayerPrefs.SetFloat("BastTime", score);
        } else
        {
            float t = PlayerPrefs.GetFloat("BastTime");
            if(t > score)
            {
                PlayerPrefs.SetFloat("BastTime", score);
            }
        }
    }

    // 存储玩家分数
    public void SaveScore(float score)
    {
        // 假设我们存储前 10 名的分数，使用 PlayerPrefs 存储分数
        for (int i = 1; i <= 10; i++)
        {
            if (!PlayerPrefs.HasKey("Score_" + i))
            {
                PlayerPrefs.SetFloat("Score_" + i, score);
                break;
            }
            else
            {
                float existingScore = PlayerPrefs.GetFloat("Score_" + i);
                if (score > existingScore)
                {
                    // 移动后面的分数，为新分数腾出位置
                    for (int j = 9; j >= i; j--)
                    {
                        PlayerPrefs.SetFloat("Score_" + (j + 1), PlayerPrefs.GetFloat("Score_" + j));
                    }
                    PlayerPrefs.SetFloat("Score_" + i, score);
                    break;
                }
            }
        }
        ShowLeaderboard();
    }


    // 显示排行榜
    public void ShowLeaderboard()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.HasKey("Score_" + i))
            {
                float score = PlayerPrefs.GetFloat("Score_" + i);
                if (leaderboardTexts.Length >= i)
                {
                    leaderboardTexts[i - 1] = score.ToString();
                }
            }
            else
            {
                hasRank = i - 1;
                break;
            }
        }
    }


    // 清除排行榜数据
    public void ClearLeaderboard()
    {
        for (int i = 1; i <= 10; i++)
        {
            PlayerPrefs.DeleteKey("Score_" + i);
        }
    }


    // 示例：在游戏结束时调用存储分数的方法
    public void GameOver(int playerScore)
    {
        SaveScore(playerScore);
        ShowLeaderboard();
    }


    // 示例：在开始菜单调用显示排行榜的方法
    public void ShowLeaderboardOnStart()
    {
        ShowLeaderboard();
    }


    // 示例：在清除数据界面调用清除排行榜的方法
    public void ClearLeaderboardOnClick()
    {
        ClearLeaderboard();
        ShowLeaderboard();
    }
}