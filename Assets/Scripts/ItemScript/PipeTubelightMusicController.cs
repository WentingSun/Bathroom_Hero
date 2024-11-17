using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTubelightMusicController : MonoBehaviour
{
    public AudioSource swingAudioSource;         // 挥舞音效的音频源
    public AudioSource collisionAudioSource;     // 碰撞音效的音频源
    public AudioClip swingSoundMusic;            // 挂载的挥舞音效文件
    public AudioClip[] collisionSounds;          // 碰撞音效数组
    public AnimationCurve speedToPitch;          // 动画曲线，用于映射速度到音高
    public AnimationCurve speedToVolume;         // 动画曲线，用于映射速度到音量
    public float maxSpeed = 8f;                  // 速度上限

    private Vector3 previousPosition;            // 上一帧的位置
    private float swingSpeed;                    // 当前速度

    private void Start()
    {
        // 初始化挥舞音效
        swingAudioSource.clip = swingSoundMusic;
        previousPosition = transform.position;

        swingAudioSource.loop = false;  // 禁止循环播放
        swingAudioSource.playOnAwake = false;  // 禁止启动时播放
    }

    private void Update()
    {
        SwingSound();
    }

    private void SwingSound()
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
        swingAudioSource.pitch = targetPitch;
        swingAudioSource.volume = targetVolume;

        // 当速度大于某个阈值且音效没有播放时播放音效
        if (swingSpeed > 1f && !swingAudioSource.isPlaying)
        {
            swingAudioSource.Play();
        }

        // 当物体静止时，停止播放音效
        if (swingSpeed <= 1f && swingAudioSource.isPlaying)
        {
            swingAudioSource.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 如果碰撞音效数组中有音效，随机播放一个
        if (collisionSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, collisionSounds.Length);
            collisionAudioSource.PlayOneShot(collisionSounds[randomIndex]);
        }
    }
}





