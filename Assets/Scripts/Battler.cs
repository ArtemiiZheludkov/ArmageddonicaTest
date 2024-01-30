using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class Battler
{
    private List<Unit> _enemies = new List<Unit>();
    private List<Unit> _allies = new List<Unit>();

    private UI UI;
    
    [Inject]
    private void Construct(UI ui)
    {
        UI = ui;
    }

    public void AddUnit(Unit unit)
    {
        if (unit.Side == UnitSide.Enemy)
        {
            _enemies.Add(unit);
        }
        else if (unit.Side == UnitSide.Ally)
        {
            _allies.Add(unit);
            UI.AddOneCreated();
        }
    }

    public Unit GetTarget(Unit unit)
    {
        List<Unit> targets = null;
        
        if (unit.Side == UnitSide.Enemy)
            targets = _allies;
        else if (unit.Side == UnitSide.Ally)
            targets = _enemies;
        
        Unit closestUnit = null;
        float closestDistance = float.MaxValue;

        foreach (Unit target in targets)
        {
            float distance = Vector3.Distance(unit.transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestUnit = target;
            }
        }

        return closestUnit;
    }

    public void UnitDead(Unit unit)
    {
        if (unit.Side == UnitSide.Enemy)
		{
			_enemies.Remove(unit);
			UI.AddOneKilled();
		}
        else if (unit.Side == UnitSide.Ally)
		{
			_allies.Remove(unit);
			UI.AddOneDied();
		}
    }
}