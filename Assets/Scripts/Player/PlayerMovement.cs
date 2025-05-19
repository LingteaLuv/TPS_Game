using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Drag&Drop")]
    [SerializeField] private Transform avatar;
    [SerializeField] private Transform aim;

    private Rigidbody _rigid;
    private PlayerStatus _playerStatus;

    [Header("Mouse Config")] 
    [SerializeField] [Range(-90, 0)] private float minPitch;
    [SerializeField] [Range(0, 90)] private float maxPitch;
    [SerializeField] [Range(0, 5)] private float mouseSensitivity;

    private Vector2 _currentRotation;
    
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        
    }

    private void Init()
    {
        _rigid = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    public Vector3 SetMove(float moveSpeed)
    {
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = _rigid.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;
        _rigid.velocity = velocity;
        return moveDirection;
    }

    public Vector3 SetAimRotation()
    {
        Vector2 mouseDir = GetMouseDirection();
        
        _currentRotation.x += mouseDir.x;
        _currentRotation.y += mouseDir.y;
        
        // x축 회전 각도 제한
        _currentRotation.y = Mathf.Clamp(_currentRotation.y, minPitch, maxPitch);
        
        // 캐릭터 오브젝트의 경우에는 y축 회전만 반영
        transform.rotation = Quaternion.Euler(0, _currentRotation.x, 0);
        
        // 에임의 경우 상하 회전 반영
        Vector3 currentEuler = aim.localEulerAngles;
        aim.localEulerAngles = new Vector3(_currentRotation.y, currentEuler.y, currentEuler.z);
        // 회전 방향 벡터 반환
        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0;
        return rotateDirVector.normalized;
    }

    public void SetAvatarRotation(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        avatar.rotation = Quaternion.Lerp(avatar.rotation, targetRotation, _playerStatus.RotateSpeed * Time.deltaTime);
    }

    private Vector3 GetMoveDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        Vector3 input = new Vector3(x, 0, z);
        Vector3 direction = (transform.right * input.x) + (transform.forward * input.z);
        return direction.normalized;
    }

    private Vector2 GetMouseDirection()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        return new Vector2(mouseX, mouseY);
    }
}
