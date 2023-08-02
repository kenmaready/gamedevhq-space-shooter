using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _scoreDisplay;
    [SerializeField] private Image _livesDisplay; 
    [SerializeField] private TextMeshProUGUI _gameOverDisplay;
    [SerializeField] private TextMeshProUGUI _restartGameDisplay;
    [SerializeField] private Sprite[] _liveSprites;
    private GameManager _gm;

    void Start()
    {
        _scoreDisplay.text = "Score: " + 0.ToString("00000");
        _livesDisplay.sprite = _liveSprites[_liveSprites.Length - 1];
        _gameOverDisplay.gameObject.SetActive(false);
        _restartGameDisplay.gameObject.SetActive(false);
        
        _gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        if (_gm == null) {
            Debug.LogError("_gam (GameManager) is Null.");
        }
    }

    void Update()
    {
    }

    public void UpdateScore(int val) {
        _scoreDisplay.text = "Score: " + val.ToString("00000");
    }

    public void UpdateLives(int livesRemaining) {
        Debug.Log("livesRemaining: " + livesRemaining + " | size of array: " + (_liveSprites.Length - 1));
        _livesDisplay.sprite = _liveSprites[livesRemaining];

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
}
