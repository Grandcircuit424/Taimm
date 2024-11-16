using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject Background;
    [SerializeField]
    GameObject RedText;
    [SerializeField]
    TextMeshProUGUI Time;
    [SerializeField]
    TextMeshProUGUI Title;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenBackstory()
    {
        Background.SetActive(true);
        Title.enabled = false;
    }

    private void Update()
    {
        Time.text = System.DateTime.Now.ToString();
    }
}
