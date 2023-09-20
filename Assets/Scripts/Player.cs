using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Player already exists!");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }
    
    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);

        if (moveDirection != Vector3.zero) {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                if (clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove) {
            Vector3 moveDirectionX = new Vector3(inputVector.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove) {
                moveDirection = moveDirectionX;
            } else {
                Vector3 moveDirectionZ = new Vector3(0, 0, inputVector.y).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove) {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (canMove) {
            transform.position += moveDistance * moveDirection;
        }

        float rotateSpeed = 20f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    
        isWalking = moveDirection != Vector3.zero;
    }

    private void SetSelectedCounter(ClearCounter clearCounter) {
        selectedCounter = clearCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounter });
    }

}
