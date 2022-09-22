using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    [HideInInspector] public bool canContinue;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject difficultyButtons;
    void Start()
    {
        startButton.SetActive(true);
    }
    public void ShowDifficultyOptions()
    {
        startButton.SetActive(false);
        difficultyButtons.SetActive(true);
    }
    public void StartGame(float difficulty)
    {
        difficultyButtons.SetActive(false);
        SceneManager.LoadScene("Level 1");
        player.gameObject.SetActive(true);
        player.difficulty = difficulty;
        player.InvokeRepeating(nameof(player.LightPowerDecreaseTick), 20f / difficulty, 8f / difficulty);
    }
}