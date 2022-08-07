using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class EndLevelMenuController : MonoBehaviour
{
    [SerializeField] private GameMainManager _gameManager;

    [SerializeField] private Text _totalLevelScore;
    [SerializeField] private Text _totalLevelTime;
    [SerializeField] private TextMeshProUGUI _gameTimerText;

    [SerializeField] private Slider _starsSlider;
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private int _scoresForTreeStars;
    private float _levelScore;
    private float _currentScoreCounter;
    private float _sliderTargetPosition;

    void OnEnable()
    {
        _totalLevelTime.text = _gameTimerText.text;

        _levelScore = _gameManager.CurrentScore;
        _currentScoreCounter = _levelScore;
        _totalLevelScore.text = _levelScore.ToString();

        foreach (var star in _stars)
        {
            star.SetActive(false);
        }

        _sliderTargetPosition = _levelScore / _scoresForTreeStars;
        if (_sliderTargetPosition > 1) _sliderTargetPosition = 1;
        _starsSlider.value = 0f;
        MoveSlider();
    }

    private async void MoveSlider()
    {
        while (true)
        {
            _currentScoreCounter -= 3;
            if (_currentScoreCounter < 0) _currentScoreCounter = 0;
            _totalLevelScore.text = _currentScoreCounter.ToString();
            float currentLerpValue = 1 - (_currentScoreCounter / _levelScore);

            _starsSlider.normalizedValue =
                Mathf.Lerp(0f, _sliderTargetPosition, currentLerpValue);
            await Task.Yield();
            if (_starsSlider.normalizedValue >= 0.35f && !_stars[0].activeSelf)
                _stars[0].SetActive(true);
            if (_starsSlider.normalizedValue >= 0.75f && !_stars[1].activeSelf)
                _stars[1].SetActive(true);
            if (_starsSlider.normalizedValue == 1f && !_stars[2].activeSelf)
                _stars[2].SetActive(true);
            if (_currentScoreCounter == 0)
                return;            
        }
    }
}
