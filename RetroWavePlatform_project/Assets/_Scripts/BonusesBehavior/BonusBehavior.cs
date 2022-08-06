using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject _effectPrefub;
    [SerializeField] private GameObject _heartPrefub;
    [SerializeField] private GameObject _coinPrefub;

    [Header("Effect Settings")]
    [SerializeField] private RectTransform _healthImageTarget;
    [SerializeField] private GameObject _healthBarAsParent;
    [SerializeField] private RectTransform _coinImageTarget;
    [SerializeField] private GameObject _scorePanelAsParent;

    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    void Start()
    {

    }

    void Update()
    {

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
                MoveHeartBonusToPanel(movingCoin);



                EventManager.OnLevelScoreChanged.Invoke(_coinBonusScores);
                break;
            case "SpeedBonus":
                EventManager.OnPlayerSpeedChanged.Invoke(_speedBonusTime,
                                                         _speedBonusDelta);
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

        GameObject effect = Instantiate(_effectPrefub,
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
                                Time.deltaTime * 1000);
            await Task.Yield();
            if (newHeart.transform.localPosition == _healthImageTarget.localPosition)
                break;
        }
        EventManager.OnPlayerHealthChanged.Invoke(_heartBonusHP);
        EventManager.OnLevelScoreChanged.Invoke(_heartBonusScores);
    }
}
