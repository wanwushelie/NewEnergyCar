using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeControl : MonoBehaviour
{
    // 公共变量，方便在Inspector中设置
    public Slider volumeSlider;  // Slider引用
    public AudioSource backgroundMusic;  // 背景音乐的AudioSource

    void Start()
    {
        // 初始化Slider的值为当前音量
        volumeSlider.value = backgroundMusic.volume;

        // 为Slider添加监听事件，值改变时自动调用方法
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // 更新音量的方法
    public void SetVolume(float volume)
    {
        backgroundMusic.volume = volume;  // 设置音量
    }
}
