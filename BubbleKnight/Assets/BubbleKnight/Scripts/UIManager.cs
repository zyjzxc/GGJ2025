using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject healthPrefab;
    public GameObject tipsPrefab;
    public Gradient healthGradient;
    
    private GameObject health;
    public List<GameObject> healthUI;

    public GameObject speedUI;
    public GameObject speedHandle;
    public GameObject speedProgress;
    public GameObject targetUI;

    public GameObject progress;

    public GameObject rankUI;
    public GameObject timeUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = transform.Find("health").gameObject;
        healthUI.Clear();
        for (int i = 0; i < GameManager.MAX_HEART; i ++)
        {
            GameObject love = Instantiate(healthPrefab, Vector3.zero, Quaternion.identity);
            love.transform.SetParent(health.transform);
            love.GetComponent<RectTransform>().localPosition = new Vector3(200 + i * 150, -172, 0); 
            healthUI.Add(love);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float healthCount)
    {
        for (int i = 0; i < healthUI.Count; i++)
        {
            if(i+1 > healthCount)
            {
                healthUI[i].GetComponent<Image>().color = Color.black;
            } else
            {
                healthUI[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void SetSpeed(float speed)
    {
        speedUI.GetComponent<TextMeshProUGUI>().text = (int)speed + " m/s";

        float progress = speed / 200;
        progress = Mathf.Clamp01(progress);
        speedProgress.GetComponent<Scrollbar>().size = progress;
        speedHandle.GetComponent<Image>().color = healthGradient.Evaluate(progress);
    }

    public void SetTarget(float now, float target)
    {
        //targetUI.GetComponent<TextMeshProUGUI>().text = (int)now + " m" + " / " + (int)target + " m";
        float p = now / target;
        p = Mathf.Clamp01(p);
        p = Mathf.Pow(p, 0.4f);
        progress.GetComponent<UIProgress>().SetProcess(p);
    }

    public void SetTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int secs = Mathf.FloorToInt(seconds % 60);
        string str = string.Format("{0:00}:{1:00}", minutes, secs);

        timeUI.GetComponent<TextMeshProUGUI>().text = str;
    }

    public void TipsText(string str, Vector3 pointWS, float scale, Color color)
    {
        // 将世界坐标转换为屏幕坐标
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(pointWS);
        // 实例化 UI 元素
        GameObject uiInstance = Instantiate(tipsPrefab, this.transform);
        uiInstance.GetComponent<TextMeshProUGUI>().text = str;
        uiInstance.GetComponent<TextMeshProUGUI>().color = color;
        uiInstance.transform.SetParent(this.transform);
        // 设置 UI 元素的位置
        RectTransform rectTransform = uiInstance.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            uiInstance.transform.position = screenPosition;
        }
        uiInstance.transform.DOScale(new Vector3(scale, scale, scale), 0.5f);//.DOShakePosition(0.5f);
        Destroy(uiInstance, 0.5f);
    }

}
