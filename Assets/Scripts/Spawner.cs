using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Vector3 _centerSpawnPosition;
    [SerializeField] private float _xSpawnRange = 9;
    [SerializeField] private float _zSpawnRange = 9;
    [SerializeField] private float _cubeSpawnDelay;
    [SerializeField] private int _poolCapacity = 8;
    [SerializeField] private int _poolMaxSize = 8;

    private ObjectPool<Cube> _pool;
    private WaitForSeconds _wait;

    private void Start()
    {
        _wait = new WaitForSeconds(_cubeSpawnDelay);
        StartCoroutine(SpawnCubeAfterDelay());
    }

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease: (cube) => ActionOnRelease(cube),
        actionOnDestroy: (cube) => Destroy(cube.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
    {
        float xOffset = UnityEngine.Random.Range(-_xSpawnRange, _xSpawnRange);
        float zOffset = UnityEngine.Random.Range(-_zSpawnRange, _zSpawnRange);
        cube.Color = _cubePrefab.GetComponent<Renderer>().sharedMaterial.color;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.transform.position = gameObject.transform.position + new Vector3(xOffset, 0, zOffset);
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(true);
        cube.BecameReady += _pool.Release;
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.BecameReady -= _pool.Release;
        cube.gameObject.SetActive(false);
    }

    private void TakeFromPool() => _pool.Get();

    private IEnumerator SpawnCubeAfterDelay()
    {
        while (true)
        {
            TakeFromPool();

            yield return _wait;
        }
    }
}
