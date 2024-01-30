using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField, Min(0)] private int _units;
    [SerializeField] private float _spawnRadius;
    [SerializeField, Min(0.1f)] private float _spawnTime;

    [Header("time between creation of one unit")]
    [SerializeField, Min(0.0f)] private float _creatingDelay;
    
    private Battler _battler;
	private DiContainer _container;

    [Inject]
    private void Construct(Battler battler, DiContainer container)
    {
        _battler = battler;
		_container = container;
    }
    
    private async void Start()
    {
        await SpawnUnitsAsync();
    }

    private async UniTask SpawnUnitsAsync()
    {
        while(true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_spawnTime));

            for (int i = 0; i < _units; i++)
            {
                SpawnUnit();
                await UniTask.Delay(TimeSpan.FromSeconds(_creatingDelay));
            }
        }
    }

    private void SpawnUnit()
    {
        Vector2 randomPoint = Random.insideUnitCircle * _spawnRadius;
        Vector3 spawnPosition = _spawnPoint.position + new Vector3(randomPoint.x, 0f, randomPoint.y);

        Unit unit = _container.InstantiatePrefab(_unitPrefab, spawnPosition, Quaternion.identity, transform).GetComponent<Unit>();
		unit.Init();
        _battler.AddUnit(unit);
    }
}
