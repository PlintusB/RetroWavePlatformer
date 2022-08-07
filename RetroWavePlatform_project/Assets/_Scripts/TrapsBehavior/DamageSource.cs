using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int _damageValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6) return;

        Vector2 damageDirection =
            collision.transform.position - transform.position;
        EventManager.OnDamageReceived.Invoke(_damageValue, damageDirection);
    }
}
