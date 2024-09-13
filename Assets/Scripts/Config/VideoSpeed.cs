using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSpeed : MonoBehaviour
{
    [SerializeField] private Slider SliderVideoSpeed;
    [SerializeField] private VideoPlayer videoPlayerAnswers;
    [SerializeField] private GameObject CanvasConfig;

    private void Start()
    {
        PlayerPrefs.SetFloat("speedVideo", 1);
        videoPlayerAnswers.playbackSpeed = PlayerPrefs.GetFloat("speedVideo");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CanvasConfig.SetActive(!CanvasConfig.activeInHierarchy);
        }
    }

    public void SetVideoSpeed()
    {
        PlayerPrefs.SetFloat("speedVideo", SliderVideoSpeed.value);
        videoPlayerAnswers.playbackSpeed = PlayerPrefs.GetFloat("speedVideo");
    }
}
