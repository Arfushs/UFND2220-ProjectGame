using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [Space]
    [Header("Level Configs")]
    [SerializeField] private GameObject _collectiblesContainer;
    [SerializeField] private int _timeInSeconds;
    
    private int _remainingTime;
    private int _neededScore;
    private int _currentScore;

    private void Awake()
    {
        _neededScore = _collectiblesContainer.transform.childCount;
        _timeSlider.maxValue = _timeInSeconds;
        _remainingTime = _timeInSeconds;
        UpdateScoreUI();
        UpdateTimeUI();
        StartCoroutine(TimerCoroutine());
    }
    
    

    private void OnEnable()
    {
        Player.OnAppleCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Player.OnAppleCollected -= UpdateScore;
    }

    private IEnumerator TimerCoroutine()
    {
        while (_remainingTime >= 0)
        {
            yield return new WaitForSeconds(1);
            _remainingTime -= 1;
            UpdateTimeUI();
        }
    }
    private void UpdateScore()
    {
        _currentScore += 1;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _scoreText.text = _currentScore.ToString() + "/" + _neededScore.ToString();
    }

    private void UpdateTimeUI()
    {
        // Yüzdelik değeri doğru hesaplayarak string'e çeviriyoruz
        float percentage = (_remainingTime / (float)_timeInSeconds) * 100;
        _timeText.text = "%" + percentage.ToString("F0"); // Tam sayı kısmını göstermek için "F0" formatı kullanılır
        _timeSlider.value = _remainingTime; // Slider değeri güncellenir
    }
    
    
}
