using UnityEngine;

public class PipeTubeCon : MonoBehaviour
{
    public PipeTubelight pipe;
    public AudioSource swingAudioSource;
    public AudioSource collisionAudioSource;
    public AudioSource BasicPipeAudioSource;

    public AudioClip swingSoundMusic;
    public AudioClip BasicPipeSound;
    public AudioClip[] collisionSounds;

    public AnimationCurve speedToPitch;
    public AnimationCurve speedToVolume;

    public float maxSpeed = 10f;
    public float swingSpeed=0f;

    public float swingPlaySpeed = 30f;
    public float swingStopSpeed = 4f;
    private bool isSwingSoundPlaying = false;

    public float basicMinVolume=0.3f;
    public float basicMaxVolume=1;
    
    private Vector3 previousPosition;

    private void Start()
    {
        //初始化AudioSource组件
        if (swingAudioSource == null)
        {
            swingAudioSource = gameObject.AddComponent<AudioSource>();
        }
        if (collisionAudioSource == null)
        {
            collisionAudioSource = gameObject.AddComponent<AudioSource>();
        }
        if (BasicPipeAudioSource == null)
        {
            BasicPipeAudioSource = gameObject.AddComponent<AudioSource>();
        }

        //基本配置挂载文件
        swingAudioSource.clip = swingSoundMusic;
        swingAudioSource.loop = true;
        swingAudioSource.playOnAwake = false;

        collisionAudioSource.loop = false;
        collisionAudioSource.playOnAwake = false;

        BasicPipeAudioSource.clip = BasicPipeSound;
        BasicPipeAudioSource.loop = true;
        BasicPipeAudioSource.playOnAwake = false;

        previousPosition = transform.position;
        swingSpeed = (transform.position - previousPosition).magnitude / Time.deltaTime;
    }

    private void Update()
{
    // 每帧只计算一次 swingSpeed
    UpdateSwingSpeed();

    // 使用计算后的 swingSpeed 值
    SwingSound();
    BasicPipeSoundPlayer();
}

private void UpdateSwingSpeed()
{
    swingSpeed = (transform.position - previousPosition).magnitude / Time.deltaTime;
    previousPosition = transform.position;
}

private void SwingSound()
{
    float normalizedSpeed = Mathf.Clamp01(swingSpeed / maxSpeed);
    swingAudioSource.pitch = speedToPitch.Evaluate(normalizedSpeed);
    swingAudioSource.volume = speedToVolume.Evaluate(normalizedSpeed);
    if (swingSpeed > swingPlaySpeed && !isSwingSoundPlaying)
    {
        swingAudioSource.Play();
        isSwingSoundPlaying = true;
    }
    else if (swingSpeed <= swingStopSpeed && isSwingSoundPlaying)
    {
        swingAudioSource.Stop();
        isSwingSoundPlaying = false;
    }
}

private void BasicPipeSoundPlayer()
{
    Debug.Log(swingSpeed); // 现在会正确输出 swingSpeed 的值
    
    if (pipe.getIsSelected())
    {
        Debug.Log("被选择");
        float normalizedSpeed = Mathf.Clamp01(swingSpeed / maxSpeed); 
        Debug.Log("标准速度："+normalizedSpeed);        
        if(normalizedSpeed!=0){
            BasicPipeAudioSource.volume = Mathf.Lerp(basicMinVolume, basicMaxVolume, normalizedSpeed);
        }else{
            BasicPipeAudioSource.volume = basicMinVolume;
        }
        if (!BasicPipeAudioSource.isPlaying)
        {
            BasicPipeAudioSource.Play();
        }
    }
    else
    {
        BasicPipeAudioSource.Stop();
    }
}
    private void OnCollisionEnter(Collision collision)
    {
        if (collisionSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, collisionSounds.Length);
            collisionAudioSource.PlayOneShot(collisionSounds[randomIndex]);
        }
    }
}

