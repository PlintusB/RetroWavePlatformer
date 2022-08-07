using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoorAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject _finishLevelPoint;
    [SerializeField] private Transform _allLevelCoinsContainer;
    private Animator _doorAnim;

    private void Awake()
    {
        _doorAnim = GetComponent<Animator>();
        _finishLevelPoint.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (_allLevelCoinsContainer.GetChild(0).gameObject.activeSelf) return;
        _doorAnim.SetTrigger("OpenTheDoor");
        _finishLevelPoint.SetActive(true);
    }
}
