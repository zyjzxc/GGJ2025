using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;
    #region Properties

    [SerializeField]
    [Tooltip("相机在三个轴向进行平移的最大位置； 默认使用旋转方式达到抖动效果； 此参数无效")]
    Vector3 maximumTranslationShake = Vector3.one * 0.5f;

    [SerializeField]
    [Tooltip("相机在三个方向进行旋转的最大角度； 默认使用旋转方式达到抖动效果； 此参数有效")]
    Vector3 maximumAngularShake = Vector3.one * 2;

    [SerializeField]
    [Tooltip("相机抖动频率")]
    float frequency = 25;


    [SerializeField]
    [Tooltip("相机由抖动状态恢复至平稳状态的速度")]
    float recoverySpeed = 1.5f;

    [SerializeField]
    [Tooltip("相机抖动强度")]
    float traumaExponent = 2;

    //获取随机种子达到每次运行时抖动的效果为随机抖动
    private float seed;
    private float trauma;

    #endregion
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        seed = UnityEngine.Random.value;
    }

    private void Update()
    {
        ShakeRotate();
    }

    /// <summary>
    /// 传入stress控制相机抖动程度
    /// 最好将赋值范围规定在 0 - 1之间
    /// 函数本身对传入数据进行了阶段操作Clamp01
    /// </summary>
    /// <param name="stress"></param>
    public void InduceStress(float stress)
    {
        trauma = Mathf.Clamp01(trauma + stress);
    }

    /// <summary>
    /// 通过位移达到抖动效果
    /// </summary>
    private void ShakeTransform()
    {
        float shake = Mathf.Pow(trauma, traumaExponent);
        transform.localPosition = new Vector3(
            maximumTranslationShake.x * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.y * (Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1),
            maximumTranslationShake.z * (Mathf.PerlinNoise(seed + 2, Time.time * frequency) * 2 - 1)
        ) * shake;
        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }

    /// <summary>
    /// 通过旋转达到抖动效果
    /// </summary>
    private void ShakeRotate()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(
            maximumAngularShake.x * (Mathf.PerlinNoise(seed + 3, Time.time * frequency) * 2 - 1),
            maximumAngularShake.y * (Mathf.PerlinNoise(seed + 4, Time.time * frequency) * 2 - 1),
            maximumAngularShake.z * (Mathf.PerlinNoise(seed + 5, Time.time * frequency) * 2 - 1)
        ) * trauma);
        trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
    }
}
