using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;
    #region Properties

    [SerializeField]
    [Tooltip("����������������ƽ�Ƶ����λ�ã� Ĭ��ʹ����ת��ʽ�ﵽ����Ч���� �˲�����Ч")]
    Vector3 maximumTranslationShake = Vector3.one * 0.5f;

    [SerializeField]
    [Tooltip("������������������ת�����Ƕȣ� Ĭ��ʹ����ת��ʽ�ﵽ����Ч���� �˲�����Ч")]
    Vector3 maximumAngularShake = Vector3.one * 2;

    [SerializeField]
    [Tooltip("�������Ƶ��")]
    float frequency = 25;


    [SerializeField]
    [Tooltip("����ɶ���״̬�ָ���ƽ��״̬���ٶ�")]
    float recoverySpeed = 1.5f;

    [SerializeField]
    [Tooltip("�������ǿ��")]
    float traumaExponent = 2;

    //��ȡ������Ӵﵽÿ������ʱ������Ч��Ϊ�������
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
    /// ����stress������������̶�
    /// ��ý���ֵ��Χ�涨�� 0 - 1֮��
    /// ��������Դ������ݽ����˽׶β���Clamp01
    /// </summary>
    /// <param name="stress"></param>
    public void InduceStress(float stress)
    {
        trauma = Mathf.Clamp01(trauma + stress);
    }

    /// <summary>
    /// ͨ��λ�ƴﵽ����Ч��
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
    /// ͨ����ת�ﵽ����Ч��
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
