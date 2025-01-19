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
        ShowLeaderboard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    void ShowRank()
    {
        for (int i = 0; i < leaderboardTexts.Length; i++)
        {
            if(9-i <= hasRank)
            {
                UIManager.instance.rankUI.transform.GetChild(9 - i).GetComponent<TextMeshProUGUI>().text = leaderboardTexts[i];
            }
            
        }
        UIManager.instance.rankUI.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = ((int)GameManager._instance.nowHeight).ToString();

    }

    // 存储玩家分数
    public void SaveScore(int score)
    {
        // 假设我们存储前 10 名的分数，使用 PlayerPrefs 存储分数
        for (int i = 1; i <= 10; i++)
        {
            if (!PlayerPrefs.HasKey("Score_" + i))
            {
                PlayerPrefs.SetInt("Score_" + i, score);
                break;
            }
            else
            {
                int existingScore = PlayerPrefs.GetInt("Score_" + i);
                if (score > existingScore)
                {
                    // 移动后面的分数，为新分数腾出位置
                    for (int j = 9; j >= i; j--)
                    {
                        PlayerPrefs.SetInt("Score_" + (j + 1), PlayerPrefs.GetInt("Score_" + j));
                    }
                    PlayerPrefs.SetInt("Score_" + i, score);
                    break;
                }
            }
        }
    }


    // 显示排行榜
    public void ShowLeaderboard()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.HasKey("Score_" + i))
            {
                int score = PlayerPrefs.GetInt("Score_" + i);
                if (leaderboardTexts.Length >= i)
                {
                    leaderboardTexts[i - 1] = "Rank " + i + ": " + score;
                }
            }
            else
            {
                hasRank = i - 1;
                break;
            }
        }

        ShowRank();
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