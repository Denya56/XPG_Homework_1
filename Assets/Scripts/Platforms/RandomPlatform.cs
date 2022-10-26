using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomPlatform : MonoBehaviour
{
    // what is the correct way to do this?
    private bool _typeSet = false;
    private int _type = -1;
    private float _travelDistance = 5;
    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        switch (_type)
        {
            case 0:
                transform.position = new Vector3(_startPosition.x + Mathf.PingPong(Time.time, _travelDistance), transform.position.y, transform.position.z);
                break;
            case 1:
                gameObject.SetActive(false);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_typeSet)
        {
            if (collision.gameObject.CompareTag(Tag.PLAYER))
            {
                SetPlatformType();
            }
        }
        if(_type == 0)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_type == 0)
        {
            collision.transform.SetParent(null);
        }
    }

    private void SetPlatformType()
    {
        _typeSet = true;
        _type = Random.Range(0, 2);
    }
}
