using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEggController : MonoBehaviour
{
    [SerializeField] private float _shotTimePeriod;
    [SerializeField] private float _plasmBallSpeed;
    [SerializeField] private int _plazmBallDamage;
    [SerializeField] private Transform _shotCreatePoint;
    [SerializeField] private GameObject _plazmBallPrefub;
    private float _currentTimerTime;
    private Animator _eggAnim;

    private void Awake()
    {
        _eggAnim = GetComponent<Animator>();
        _currentTimerTime = _shotTimePeriod;
    }

    void Update()
    {
        _currentTimerTime -= Time.deltaTime;
        if(_currentTimerTime <= 0)
        {
            _eggAnim.SetTrigger("Shot");
            _currentTimerTime = _shotTimePeriod;
        }
    }

    public void CreateShot()
    {
        GameObject newPlasmBall =
            Instantiate(_plazmBallPrefub,
                        _shotCreatePoint.position,
                        gameObject.transform.rotation);
        newPlasmBall.GetComponent<PlazmBallController>()
                    .PlazmBallDamage = _plazmBallDamage;
        newPlasmBall.GetComponent<Rigidbody2D>().velocity =
            -transform.right * _plasmBallSpeed;
    }
}
