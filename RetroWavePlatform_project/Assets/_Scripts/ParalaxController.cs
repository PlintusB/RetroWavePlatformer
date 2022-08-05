using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    [SerializeField] private Transform[] _layers;
    [SerializeField] private float[] _speedLayersCoeffs;
    private int _layersLength;

    private void Start()
    {
        _layersLength = _layers.Length;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _layersLength; i++)
        {
            _layers[i].position =
                new Vector3(transform.position.x,
                transform.position.y, 0)
                * _speedLayersCoeffs[i];
        }
            
    }
}
