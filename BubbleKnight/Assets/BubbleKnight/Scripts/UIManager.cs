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
    public GameObject speedProgress;
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
                Debug.Log("修改血量成黑色");
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
    }

    public void SetTarget(float now, float target)
    {
        targetUI.GetComponent<TextMeshProUGUI>().text = (int)now + " m" + " / " + (int)target + " m";
        progress.GetComponent<UIProgress>().SetProcess(now/target);
    }

}
