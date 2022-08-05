using UnityEngine;

namespace PlayerControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private float _runSpeed;

        public Rigidbody2D PlayerRb { get; private set; }
        private int _jumpIndex;
        [SerializeField] private float _koyoteTime;
        private float _currentKoyoteTime;

        [SerializeField] private float _jumpPower;


        private void Awake()
        {
            PlayerRb = GetComponent<Rigidbody2D>();
            _jumpIndex = 0;
        }

        void Start()
        {
            //Time.timeScale = 0.2f;
        }

        void FixedUpdate()
        {
            Run(_playerManager.XMoveInput);
        }

        private void Update()
        {
            Jump();
            CheckCoyoteTime();
            Falling();

        }

        private void Run(float horizontalInput)
        {
            if (horizontalInput == 0)
            {
                PlayerRb.velocity = new Vector2(0, PlayerRb.velocity.y);
                return;
            }
 
            float move = horizontalInput
                * _runSpeed 
                * Time.fixedDeltaTime;
            PlayerRb.velocity = new Vector2(move, PlayerRb.velocity.y);
        }

        private void Jump()
        {
            if(Mathf.Abs(PlayerRb.velocity.y) < 0.01f) 
                _jumpIndex = 0;
            if (!_playerManager.IsJumpButtonPressed)
                return;
            if (_currentKoyoteTime < 0 && _jumpIndex  == 0)
                return;
            if (_jumpIndex > 1)
                return;

            PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, 0);
            PlayerRb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _jumpIndex++;
        }

        private void CheckCoyoteTime()
        {
            if (_playerManager.IsGrounded)
                _currentKoyoteTime = _koyoteTime;                
            else
                _currentKoyoteTime -= Time.deltaTime;
        }

        private void Falling()
        {
            if(PlayerRb.velocity.y < -0.01)
            {
                float acceleration = 5f * Time.deltaTime;
                PlayerRb.velocity =
                    new Vector2(PlayerRb.velocity.x,
                    PlayerRb.velocity.y - acceleration);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Default Values")]
        public void ResetValues()
        {

        }
#endif

    }

}


//private float runSensitivity;


//    jumpPower = 3f; // mass = 1;
//    runSensitivity = 2f;

//public void Move(float direction, bool isJumpButtonPressed)
//{
//    if (isJumpButtonPressed) Jump();
//    if (direction != 0) Run(direction);
//    if (Mathf.Abs(direction) < 0.3f && isGrounded)
//        rb.velocity = new Vector2(0, rb.velocity.y);
//}

//public void Run(float direction)
//{
//    rb.velocity = new Vector2
//        (curve.Evaluate(direction * runSensitivity), rb.velocity.y);
//}

//public void Jump()
//{
//    if (isGrounded || jumpIndex == 0)
//    {
//        rb.velocity = new Vector2(rb.velocity.x, 0);
//        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
//        jumpIndex++;
//    }
//}