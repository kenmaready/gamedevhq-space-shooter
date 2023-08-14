using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _isGameOver;
    public bool isCoopMode = false;
    private int _highScore;

    private void Awake() {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void GameOver() {
        _isGameOver = true;
    }

    private void Update() {
        if (_isGameOver) {
            if (Input.GetKeyDown(KeyCode.R)) {
                RestartGame();
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                    SceneManager.LoadScene(0);
                    return;
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }
        }
    }

    public void PauseGame() {
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
    }

    private void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    public int GetHighScore() {
        Debug.Log("Returning High Score of " + _highScore);
        return _highScore;
    }

    public void UpdateHighScore(int val) {
        _highScore = val;
        PlayerPrefs.SetInt("HighScore", _highScore);
    }
}
