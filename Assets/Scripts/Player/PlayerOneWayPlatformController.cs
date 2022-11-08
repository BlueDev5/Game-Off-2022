using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatformController : MonoBehaviour
{
    private Collider2D _currentOneWayPlatform = null;
    private Collider2D _playerCollider;

    void Awake()
    {
        _playerCollider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<PlatformEffector2D>(out PlatformEffector2D effector))
        {
            // This is a one way platform.
            if (effector.useOneWay)
            {
                _currentOneWayPlatform = other.collider;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        _currentOneWayPlatform = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (_currentOneWayPlatform == null) return;

            StartCoroutine(DisableCollision());
        }
    }

    private IEnumerator DisableCollision()
    {
        var _lastPlatform = _currentOneWayPlatform;
        Physics2D.IgnoreCollision(_playerCollider, _lastPlatform);

        // * This number requires tweaking based on the thickness of the platforms.
        yield return new WaitForSeconds(0.5f);

        Physics2D.IgnoreCollision(_playerCollider, _lastPlatform, false);
    }
}
