using System.Threading.Tasks;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private float _runSpeed;
        [SerializeField] private GameObject _speedBoostTrail;

        public Rigidbody2D PlayerRb { get; private set; }
        private int _jumpIndex;
        [SerializeField] private float _koyoteTime;
        private float _currentKoyoteTime;

        [SerializeField] private float _jumpPower;


        private void Awake()
        {
            PlayerRb = GetComponent<Rigidbody2D>();
            _jumpIndex = 0;

            EventManager.OnPlayerSpeedChanged.AddListener(GetEffectFromSpeedBonus);
            _speedBoostTrail.SetActive(false);
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

        private async void GetEffectFromSpeedBonus(float time, float delta)
        {
            _runSpeed *= delta;
            _speedBoostTrail.SetActive(true);
            await Task.Delay(Mathf.RoundToInt(time * 1000f));
            _runSpeed /= delta;
            _speedBoostTrail.SetActive(false);
        }


#if UNITY_EDITOR
        [ContextMenu("Default Values")]
        public void ResetValues()
        {

        }
#endif
    }
}
