using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;
    public Vector2 MoveInput { get; private set; }
    public bool ShootJustPressed { get; private set; }

    private PlayerInput _playerInput;
    private InputAction shootAction;
    private InputAction moveAction;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();

        SetUpInputAction();
    }

    public void SetUpInputAction()
    {
        shootAction = _playerInput.actions["Shoot"];
        moveAction = _playerInput.actions["Move"];
    }

    private void UpdateInputs()
    {
        MoveInput = moveAction.ReadValue<Vector2>().normalized;
        ShootJustPressed = shootAction.WasPressedThisFrame();
    }

    void Update()
    {
        UpdateInputs();
    }

}
