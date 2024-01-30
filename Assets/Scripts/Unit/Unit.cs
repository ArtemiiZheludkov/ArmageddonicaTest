using ProjectDawn.Navigation.Hybrid;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AgentAuthoring))]
public class Unit : MonoBehaviour
{    
    public UnitSide Side;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackRange;
    [SerializeField] private AgentAuthoring _agent;

    private bool _isAlive;
    private Battler _battler;
    private Unit _target;

    [Inject]
    private void Construct(Battler battler)
    {
        _battler = battler;
    }
    
    public void Init()
    {
        _isAlive = true;
		var locomotion = _agent.EntityLocomotion;
		locomotion.Speed = _moveSpeed;
		_agent.EntityLocomotion = locomotion;
    }
	
	private void FixedUpdate()
	{
		if (_isAlive == false)
			return;
		
		if (_target == null)
		{
			SetTarget(_battler.GetTarget(this));
			
			if(_target == null)
				_agent.Stop();
			
			return;
		}
        
        float distance = Vector3.Distance(transform.position, _target.transform.position);

        if (distance <= _attackRange)
			_target.Attack();
		else
			_agent.SetDestination(_target.transform.position);
	}

	public void SetTarget(Unit target)
	{
		if (target == null)
			return;
		
		_target = target;
		_target.FightWithMe(this);
	}

	public void FightWithMe(Unit target)
	{
		if (target == _target || target == null)
			return;

		if (_target == null)
		{
			_target = target;
			return;
		}

		float currentTargetDistance = Vector3.Distance(transform.position, _target.transform.position);
		float variantTargetDistance = Vector3.Distance(transform.position, target.transform.position);
		
		if (currentTargetDistance < variantTargetDistance)
			_target = target;
	}

	public void TakeDamage()
    {
		if (_isAlive == false)
			return;
		
        Death();
    }
    
    private void Attack()
    {
	    if (_target == null)
	    {
		    SetTarget(_battler.GetTarget(this));
		    return;
	    }
		
	    _target.TakeDamage();
	    Death();
    }

    private void Death()
    {
        _isAlive = false;
		_agent.Stop();
        _battler.UnitDead(this);
		gameObject.SetActive(false);
        Destroy(gameObject);
    }
}