using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    [SerializeField] private TextMeshProUGUI _highScoreDisplay;
    [SerializeField] private Image _livesDisplay; 
    [SerializeField] private TextMeshProUGUI _gameOverDisplay;
    [SerializeField] private TextMeshProUGUI _restartGameDisplay;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Sprite[] _liveSprites;

    private GameManager _gm;
    private Animator _pausePanelAnimator;

    private void Awake() {
        _gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        if (_gm == null) {
            Debug.LogError("_gam (GameManager) is Null.");
        }
    }

    void Start()
    {
        _scoreDisplay.text = "Score: " + 0.ToString("00000");
        _livesDisplay.sprite = _liveSprites[_liveSprites.Length - 1];
        _gameOverDisplay.gameObject.SetActive(false);
        _restartGameDisplay.gameObject.SetActive(false);
        _pausePanel.gameObject.SetActive(true);
        _highScoreDisplay.text = "High Score: " + _gm.GetHighScore().ToString("000000");

        _pausePanelAnimator = _pausePanel.GetComponent<Animator>();
        if (_pausePanelAnimator == null) {
            Debug.LogError("Animantor Component not found on Pause Panel Menu.");
        }
    }

    void Update()
    {
            if (!_gm._isGameOver && Input.GetKeyDown(KeyCode.P)) {
                PauseGame();
            }
    }

    public void UpdateScore(int val) {
        _scoreDisplay.text = "Score: " + val.ToString("00000");
        if (val > _gm.GetHighScore()) {
            _gm.UpdateHighScore(val);
            _highScoreDisplay.text = "High Score: " + val.ToString("000000");
        }
    }

    public void UpdateLives(int livesRemaining) {
        _livesDisplay.sprite = _liveSprites[Math.Max(livesRemaining,0)];

        if (livesRemaining <= 0) {
            GameOverSequence();
        }
    }

    void GameOverSequence() {
        ActivateGameOverDisplay();
        ActivateRestartGameDisplay();
        _gm.GameOver();
    }

    void ActivateGameOverDisplay() {
        StartCoroutine(FlickerGameOverDisplay());
    }

    IEnumerator FlickerGameOverDisplay() {
        bool DisplayOn = true;

        while(true) {
            _gameOverDisplay.gameObject.SetActive(DisplayOn);
            DisplayOn = !DisplayOn;

            yield return new WaitForSeconds(0.4f);
        }
    }

    void ActivateRestartGameDisplay() {
        _restartGameDisplay.gameObject.SetActive(true);
    }

    public void ReturnToMainMenu() {
        _gm.LoadMainMenu();
        _gm.ResumeGame();
    }

    void PauseGame() {
        _pausePanel.SetActive(true);
        _pausePanelAnimator.SetBool("isPaused", true);
        _gm.PauseGame();
    }

    public void ResumeGame() {
        _gm.ResumeGame();
        _pausePanelAnimator.SetBool("isPaused", false);
        _pausePanel.gameObject.SetActive(false);
    }
}
