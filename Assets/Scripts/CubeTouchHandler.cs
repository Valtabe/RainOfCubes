using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTouchHandler : MonoBehaviour
{
    public event Action PlatformTouched;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            PlatformTouched?.Invoke();
        }
    }
}
