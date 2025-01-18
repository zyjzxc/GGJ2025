using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<GameObject> healthUI;

    public GameObject speedUI;
    public GameObject targetUI;

    public GameObject progress;
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
                Debug.Log("�޸�Ѫ���ɺ�ɫ");
            } else
            {
                healthUI[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void SetSpeed(float speed)
    {
        speedUI.GetComponent<TextMeshProUGUI>().text = "Speed: " + speed.ToString("0.0") + " m/s";
    }

    public void SetTarget(float now, float target)
    {
        targetUI.GetComponent<TextMeshProUGUI>().text = (int)now + " m" + " / " + (int)target + " m";
        progress.GetComponent<UIProgress>().SetProcess(now/target);
    }

}
