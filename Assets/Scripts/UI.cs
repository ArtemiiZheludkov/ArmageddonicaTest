using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _killedTMP;
    [SerializeField] private TMP_Text _createdTMP;
    [SerializeField] private TMP_Text _diedTMP;

    private int _killed, _created, _died;

    private void Start()
    {
        _killed = 0;
        _created = 0;
        _died = 0;
        
        _killedTMP.text = _killed.ToString();
        _createdTMP.text = _created.ToString();
        _diedTMP.text = _died.ToString();
    }

    public void AddOneKilled()
    {
        _killed += 1;
        _killedTMP.text = _killed.ToString();
    }
    
    public void AddOneCreated()
    {
        _created += 1;
        _createdTMP.text = _created.ToString();
    }
    
    public void AddOneDied()
    {
        _died += 1;
        _diedTMP.text = _died.ToString();
    }
}