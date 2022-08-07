using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlazmBallController : MonoBehaviour
{
    private GameObject _plazmaBallFlyingState;
    private GameObject _plazmaBallHitState;
    public int PlazmBallDamage { get; set; }
    private Rigidbody2D _plazmBallRb;

    void Awake()
    {
        _plazmBallRb = GetComponent<Rigidbody2D>();
        _plazmaBallFlyingState = transform.GetChild(0).gameObject;
        _plazmaBallHitState = transform.GetChild(1).gameObject;
        _plazmaBallFlyingState.SetActive(true);
        _plazmaBallHitState.SetActive(false);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return;

        _plazmBallRb.velocity = Vector2.zero;
        _plazmaBallFlyingState.SetActive(false);
        _plazmaBallHitState.SetActive(true);

        Vector2 damageDirection =
            collision.transform.position - transform.position;
        EventManager.OnDamageReceived.Invoke(PlazmBallDamage, damageDirection);
        Destroy(gameObject, 0.6f);
    }
}
