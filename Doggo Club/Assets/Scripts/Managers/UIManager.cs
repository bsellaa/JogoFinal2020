using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavGame.Managers;

public class UIManager : MonoBehaviour
{
    public GameObject defeatPanel;
    public GameObject victoryPanel;
    public Text errorText;
    public float errorTime = 1.5f;
    public Text coinText;
    public Text waveCountText;
    public Text waveCountdownText;
    public GameObject[] cooldownObjects;
    public Text[] actionCosts;
    Image[] cooldownImages;

    void OnEnable()
    {
        LevelManager.instance.onDefeat += OnDefeat;
        LevelManager.instance.onVictory += OnVictory;
    }

    void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {

    }

    void OnResourceUpdate(int currentAmount)
    {
        coinText.text = "x " + currentAmount;
    }

    void OnWaveUpdate(int totalWaves, int currentWave)
    {
        waveCountText.text = currentWave + " / " + totalWaves;
    }

    void OnWaveCountdown(float remainingTime)
    {
        waveCountdownText.text = remainingTime.ToString("F1");
    }

    void OnDefeat()
    {
        LevelManager.instance.Pause();
        defeatPanel.SetActive(true);
    }

    void OnVictory()
    {
        LevelManager.instance.Pause();
        victoryPanel.SetActive(true);
    }

    public void OnBtReloadClick()
    {
        LevelManager.instance.Resume();
        NavigationManager.instance.ReloadScene();
    }

    public void OnBtExitClick()
    {
        LevelManager.instance.Resume();
        NavigationManager.instance.LoadScene("Home");
    }
}
