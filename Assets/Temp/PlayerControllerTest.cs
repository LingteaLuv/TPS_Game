using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 충돌이 일어나지 않게끔 하기 위한 꼼수
namespace LSW_Test
{
    /// <summary>
    /// Movement 테스트 용으로 구현한 클래스
    /// controller 구현하시는 분께서 movement 호출 관련 메서드 정리 끝나시면
    /// 해당 파일은 삭제해도 됩니다.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [Header("Drag&Drop")]
        public PlayerMovement movement;
        public PlayerStatus status;
        
        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            MoveTest();

            status.IsAiming.Value = Input.GetKey(KeyCode.Mouse1);
        }

        private void Init()
        {
        
        }
        
        /// <summary>
        /// 아래 메서드에 적힌 소스코드와 같은 방식으로 작동합니다.
        /// </summary>
        public void MoveTest()
        {
            // (회전 수행 후)좌우 회전에 대한 벡터 반환
            Vector3 camRotateDir = movement.SetAimRotation();

            float moveSpeed;
            if (status.IsAiming.Value) moveSpeed = status.WalkSpeed;
            else moveSpeed = status.RunSpeed;

            Vector3 moveDir = movement.SetMove(moveSpeed);
            status.IsMoving.Value = (moveDir != Vector3.zero);

            Vector3 avatarDir;
            if (status.IsAiming.Value) avatarDir = camRotateDir;
            else avatarDir = moveDir;
            
            movement.SetAvatarRotation(avatarDir);
        }
    }
}

