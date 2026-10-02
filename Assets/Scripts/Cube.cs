using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minReleaseDelay = 2f;
    [SerializeField] private float _maxReleaseDelay = 5f;

    public event Action<Cube> BecameReady;

    private bool _isPlatformTouched;
    private WaitForSeconds _wait;

    private void OnEnable()
    {
        float delay = UnityEngine.Random.Range(_minReleaseDelay, _maxReleaseDelay);
        _wait = new WaitForSeconds(delay);
        _isPlatformTouched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPlatformTouched) return;

        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _isPlatformTouched = true;
            this.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
            StartCoroutine(GetReadyToRelease());
        }
    }

    private IEnumerator GetReadyToRelease()
    {
        yield return _wait;

        BecameReady?.Invoke(this);
    }
}
