using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

namespace Capabilities
{
    public class Move : MonoBehaviour
    {
        [Separator("Movement Attributes")]
        [SerializeField] private float maxSpeedXY = 4f;
        [SerializeField] private float maxAcceleration = 35f;

        [Separator("Test Attributes")]
        [SerializeField] private float groundFriction = 0.9f;
        [SerializeField] private Vector2 _velocity, _direction, _desiredVelocity;

        private Rigidbody2D _body;
        private Animator _animator;

        private float _maxSpeedChange;
        public bool _movementActive;
        private readonly int _walkingHorizontal = Animator.StringToHash("Horizontal");
        private readonly int _walkingVertical = Animator.StringToHash("Vertical");
        private readonly int _walkingSpeed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Start()
        {

            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            _maxSpeedChange = maxAcceleration * Time.deltaTime;
            _velocity = Vector2.MoveTowards(_velocity, _desiredVelocity, _maxSpeedChange);
            _animator.SetFloat(_walkingSpeed, _velocity.sqrMagnitude);

            _body.velocity = _velocity;

        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                _direction = value.ReadValue<Vector2>();
                _animator.SetFloat(_walkingHorizontal, _direction.x);
                _animator.SetFloat(_walkingVertical, _direction.y);
                _desiredVelocity = _direction * Mathf.Max(maxSpeedXY - groundFriction, 0f);
            } 
            else if (value.canceled) 
            {
                _direction = Vector2.zero;
                _desiredVelocity = Vector2.zero;
            }
 

        }

    }
}
