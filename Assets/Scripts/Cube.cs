using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private CubeTouchHandler _touchHandler;
    [SerializeField] private float _minReleaseDelay = 2f;
    [SerializeField] private float _maxReleaseDelay = 5f;

    public event Action<Cube> BecameReady;

    private bool _isPlatformTouched;
    private WaitForSeconds _wait;

    public Color Color 
    { 
        get
        {
            return GetComponent<Renderer>().material.color;
        }

        set
        {
            _colorChanger.SetCustomColor(value);
        }
    }

    private void OnEnable()
    {
        float delay = UnityEngine.Random.Range(_minReleaseDelay, _maxReleaseDelay);
        _wait = new WaitForSeconds(delay);
        _isPlatformTouched = false;
        _touchHandler.PlatformTouched += StartWaitingToRelease;
    }

    private void OnDisable()
    {
        _touchHandler.PlatformTouched -= StartWaitingToRelease;
    }

    private void StartWaitingToRelease()
    {
        if (_isPlatformTouched) 
            return;

        _colorChanger.SetRandomColor();
        _isPlatformTouched = true;
        StartCoroutine(GetReadyToRelease());
    }

    private IEnumerator GetReadyToRelease()
    {
        yield return _wait;

        BecameReady?.Invoke(this);
    }
}
