using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearingPlatform : MonoBehaviour
{
    [SerializeField] private float _toggleTime;
    private float _currentTime;
    private bool _enabled;
    void Start()
    {
        _enabled = true;
    }

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _toggleTime)
        {
            _currentTime = 0;
            TogglePlatform();
        }
    }

    private void TogglePlatform()
    {
        _enabled = !_enabled;
        gameObject.GetComponent<TilemapRenderer>().enabled = _enabled;
        gameObject.GetComponent<TilemapCollider2D>().enabled = _enabled;
    }
}
