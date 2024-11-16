using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTubelightMusicController : MonoBehaviour
{

    public AudioSource audioSource;        // 音频源
    public AudioClip swingSound;           // 挂载的音效文件
    public AnimationCurve speedToPitch;    // 动画曲线，用于映射速度到音高
    public AnimationCurve speedToVolume;   // 动画曲线，用于映射速度到音量
    public float maxSpeed = 8f;           // 速度上限

    private Vector3 previousPosition;      // 上一帧的位置
    private float swingSpeed;              // 当前速度

    private void Start()
    {
        // 获取 AudioSource 组件，如果没有就添加一个
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // 确保音效文件已经被挂载到 swingSound 字段
        if (swingSound != null)
        {
            audioSource.clip = swingSound;
        }
        else
        {
            Debug.LogError("Swing sound is not assigned! Please assign an AudioClip.");
        }

        previousPosition = transform.position;

        // 配置音频源
        audioSource.loop = false;  // 禁止循环播放
        audioSource.playOnAwake = false;  // 禁止启动时播放
    }

    private void Update()
    {
        // 计算挥舞速度
        swingSpeed = (transform.position - previousPosition).magnitude / Time.deltaTime;
        previousPosition = transform.position;

        // 将速度归一化到 [0, 1]
        float normalizedSpeed = Mathf.Clamp01(swingSpeed / maxSpeed);

        // 根据曲线映射音高和音量
        float targetPitch = speedToPitch.Evaluate(normalizedSpeed);
        float targetVolume = speedToVolume.Evaluate(normalizedSpeed);

        // 设置音效的音高和音量
        audioSource.pitch = targetPitch;
        audioSource.volume = targetVolume;

        // 当速度大于某个阈值且音效没有播放时播放音效
        if (swingSpeed > 1f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        // 当物体静止时，停止播放音效
        if (swingSpeed <= 1f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}




