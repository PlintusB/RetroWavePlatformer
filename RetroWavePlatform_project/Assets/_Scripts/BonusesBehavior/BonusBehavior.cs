using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print(_healthBarRect.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "HeartBonus":
                EventManager.OnHeartBonusTook.Invoke(_heartBonusScores,
                                                     _heartBonusHP);
                Destroy(collision.gameObject);
                break;
            case "CoinBonus":
                EventManager.OnCoinBonusTook.Invoke(_coinBonusScores);

                print("CoinBonus");
                break;
            case "SpeedBonus":
                EventManager.OnSpeedBonusTook.Invoke(_speedBonusScores,
                                                     _speedBonusTime,
                                                     _speedBonusDelta);

                print("SpeedBonus");
                break;
            case "ImmortalBonus":
                EventManager.OnImmortalBonusTook.Invoke(_immortalBonusScores,
                                                        _immortalBonusTime);

                print("ImmortalBonus");
                break;
            default:
                print("SOMETHING");
                break;
        }

        GameObject effect =
            Instantiate(_effectPrefub,
                        collision.transform.position,
                        transform.rotation);
        Destroy(effect, 0.5f);
    }

}
