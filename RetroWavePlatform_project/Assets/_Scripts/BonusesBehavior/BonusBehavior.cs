using System.Threading.Tasks;
using UnityEngine;

public class BonusBehavior : MonoBehaviour
{
    [Header("HeartBonus values")]
    [SerializeField] private int _heartBonusScores;
    [SerializeField] private int _heartBonusHP;

    [Header("CoinBonus values")]
    [SerializeField] private int _coinBonusScores;

    [Header("SpeedBonus values")]
    [SerializeField] private int _speedBonusScores;
    [SerializeField] private float _speedBonusTime;
    [SerializeField] private float _speedBonusDelta;

    [Header("ImmortalBonus values")]
    [SerializeField] private int _immortalBonusScores;
    [SerializeField] private float _immortalBonusTime;

    [Header("Prefubs")]
    [SerializeField] private GameObject _takeBonusEffectPrefub;
    [SerializeField] private GameObject _scorePanelEffectPrefub;
    [SerializeField] private GameObject _HPPanelEffectPrefub;
    [SerializeField] private GameObject _heartPrefub;
    [SerializeField] private GameObject _coinPrefub;

    [Header("Effect Settings")]
    [SerializeField] private int _heartBonusMoveSpeed;
    [SerializeField] private int _coinBonusMoveSpeed;
    [SerializeField] private RectTransform _healthImageTarget;
    [SerializeField] private GameObject _healthBarAsParent;
    [SerializeField] private RectTransform _coinImageTarget;
    [SerializeField] private GameObject _scorePanelAsParent;
    [SerializeField] private GameObject _scorePanelEffectPoint;
    [SerializeField] private GameObject _HPEffectPoint;

    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "HeartBonus":
                Vector3 posBonusToScreen =
                    _mainCam.WorldToScreenPoint(collision.transform.position);
                GameObject movingHeart =
                    Instantiate(_heartPrefub,
                                posBonusToScreen,
                                collision.transform.rotation,
                                _healthBarAsParent.transform);
                MoveHeartBonusToPanel(movingHeart);
                break;
            case "CoinBonus":
                posBonusToScreen =
                    _mainCam.WorldToScreenPoint(collision.transform.position);
                GameObject movingCoin =
                    Instantiate(_coinPrefub,
                                posBonusToScreen,
                                collision.transform.rotation,
                                _scorePanelAsParent.transform);
                MoveCoinBonusToPanel(movingCoin);
                break;
            case "SpeedBonus":
                EventManager.OnPlayerSpeedChanged
                    .Invoke(_speedBonusTime, _speedBonusDelta);
                EventManager.OnLevelScoreChanged.Invoke(_speedBonusScores);
                break;
            case "ImmortalBonus":
                EventManager.OnImmortalStatusChanged.Invoke(_immortalBonusTime);
                EventManager.OnLevelScoreChanged.Invoke(_immortalBonusScores);
                break;
            default:
                return;
        }

        Destroy(collision.gameObject);
        GameObject effect = Instantiate(_takeBonusEffectPrefub,
                                        collision.transform.position,
                                        transform.rotation);
        Destroy(effect, 0.5f);
    }

    async void MoveHeartBonusToPanel(GameObject newHeart)
    {
        while (true)
        {
            newHeart.transform.localPosition =
            Vector2.MoveTowards(newHeart.transform.localPosition,
                                _healthImageTarget.localPosition,
                                Time.deltaTime * _heartBonusMoveSpeed);
            await Task.Yield();
            if (newHeart.transform.localPosition == _healthImageTarget.localPosition)
                break;
        }
        EventManager.OnPlayerHealthChanged.Invoke(_heartBonusHP);
        EventManager.OnLevelScoreChanged.Invoke(_heartBonusScores);
        Destroy(newHeart);
        ActivateHPIncreaseEffect();
    }

    async void MoveCoinBonusToPanel(GameObject newCoin)
    {
        while (true)
        {
            newCoin.transform.localPosition =
            Vector2.MoveTowards(newCoin.transform.localPosition,
                                _coinImageTarget.localPosition,
                                Time.deltaTime * _coinBonusMoveSpeed);
            await Task.Yield();
            if (newCoin.transform.localPosition == _coinImageTarget.localPosition)
                break;
        }
        EventManager.OnLevelScoreChanged.Invoke(_coinBonusScores);
        Destroy(newCoin);
        ActivateScoreIncreaseEffect();
    }

    void ActivateScoreIncreaseEffect()
    {
        GameObject scoreEffect =
            Instantiate(_scorePanelEffectPrefub,
                        _scorePanelEffectPoint.transform);
        Destroy(scoreEffect, 0.5f);
    }

    void ActivateHPIncreaseEffect()
    {
        GameObject healthEffect =
            Instantiate(_HPPanelEffectPrefub,
                        _HPEffectPoint.transform);
        Destroy(healthEffect, 0.5f);
    }
}
