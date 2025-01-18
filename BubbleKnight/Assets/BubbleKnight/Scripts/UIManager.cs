using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public List<GameObject> health;

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
        for (int i = 0; i < health.Count; i++)
        {
            if(i+1 > healthCount)
            {
                health[i].GetComponent<Image>().color = Color.black;
                Debug.Log("修改血量成黑色");
            } else
            {
                health[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
