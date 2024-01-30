using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 15f;

    private float _horizontal = 0f;
    private float _vertical = 0f;

    private string _horizontalName = "Horizontal";
    private string _verticalName = "Vertical";

    private void Update()
    {
        _horizontal = Input.GetAxis(_horizontalName);
        _vertical = Input.GetAxis(_verticalName);
        
        transform.position += new Vector3(_horizontal, 0, _vertical) * _moveSpeed * Time.deltaTime;
    }
}
