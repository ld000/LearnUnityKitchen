using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private GameInput gameInput;
    private bool isWalking;
    
    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        float rotateSpeed = 20f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    
        isWalking = moveDirection != Vector3.zero;
    }

    public bool IsWalking() {
        return isWalking;
    }

}
