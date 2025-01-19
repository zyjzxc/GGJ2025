using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LocalLeaderboard : MonoBehaviour
{
    public static LocalLeaderboard instance;
    public string[] leaderboardTexts = new string[10]; // ������ʾ���а�� UI �ı�Ԫ��

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

    // �洢��ҷ���
    public void SaveScore(int score)
    {
        // �������Ǵ洢ǰ 10 ���ķ�����ʹ�� PlayerPrefs �洢����
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
                    // �ƶ�����ķ�����Ϊ�·����ڳ�λ��
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


    // ��ʾ���а�
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


    // ������а�����
    public void ClearLeaderboard()
    {
        for (int i = 1; i <= 10; i++)
        {
            PlayerPrefs.DeleteKey("Score_" + i);
        }
    }


    // ʾ��������Ϸ����ʱ���ô洢�����ķ���
    public void GameOver(int playerScore)
    {
        SaveScore(playerScore);
        ShowLeaderboard();
    }


    // ʾ�����ڿ�ʼ�˵�������ʾ���а�ķ���
    public void ShowLeaderboardOnStart()
    {
        ShowLeaderboard();
    }


    // ʾ������������ݽ������������а�ķ���
    public void ClearLeaderboardOnClick()
    {
        ClearLeaderboard();
        ShowLeaderboard();
    }
}