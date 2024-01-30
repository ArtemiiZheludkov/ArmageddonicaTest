using UnityEngine;
using Zenject;

public class ClickSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Transform _unitsContainer;
    
    private Camera _camera;
    private Battler _battler;
	private DiContainer _container;

    [Inject]
    private void Construct(Battler battler, DiContainer container)
    {
        _battler = battler;
		_container = container;
    }
    
    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) == true)
                SpawnUnit(hit.point);
        }
    }

    private void SpawnUnit(Vector3 spawnPosition)
    {
        Unit unit = _container.InstantiatePrefab(_unitPrefab, spawnPosition, Quaternion.identity, _unitsContainer).GetComponent<Unit>();
		unit.Init();
        _battler.AddUnit(unit);
    }
}