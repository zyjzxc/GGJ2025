using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgress : MonoBehaviour
{
    [Range(0, 1)]
    public float progress = 0;
    private RectTransform childRectTransform;
    RectTransform rectTransform;
    float maxHeight = 940;
    // Start is called before the first frame update
    void Start()
    {
        childRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        //maxHeight = rectTransform.rect.height - childRectTransform.rect.height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        childRectTransform.anchoredPosition = new Vector3(0, progress * maxHeight, 0);
    }

    public void SetProcess(float p)
    {
        p = Mathf.Clamp01(p);
        progress = p;
    }
}
