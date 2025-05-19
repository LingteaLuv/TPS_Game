using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 충돌이 일어나지 않게끔 하기 위한 꼼수
namespace LSW_Test
{
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
        }

        private void Init()
        {
        
        }
        
        public void MoveTest()
        {
            // (회전 수행 후)좌우 회전에 대한 벡터 반환
            Vector3 camRotateDir = movement.SetAimRotation();

            float moveSpeed;
            if (status.IsAiming.Value) moveSpeed = status.WalkSpeed;
            else moveSpeed = status.RunSpeed;

            Vector3 moveDir = movement.SetMove(moveSpeed);
            status.IsMoving.Value = (moveDir != Vector3.zero);
            
            // Todo : 몸체의 회전 구현 후 호춯
            
        }
    }
}

