using System.Threading.Tasks;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private float _runSpeed;
        private float _defaultRunSpeed;
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
            _defaultRunSpeed = _runSpeed;

            EventManager.OnPlayerSpeedChanged.AddListener(GetEffectFromSpeedBonus);
            _speedBoostTrail.SetActive(false);

            EventManager.OnDamageReceived.AddListener(GetEffectFromDamage);
        }

        void Start()
        {
            //Time.timeScale = 0.2f;
        }

        void FixedUpdate()
        {
            if (_runSpeed == 0) return;
            Run(_playerManager.XMoveInput);
        }

        private void Update()
        {
            Jump();
            CheckCoyoteTime();
            FallingAcceleration();
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

        private void FallingAcceleration()
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
            _defaultRunSpeed *= delta;
            _runSpeed = _defaultRunSpeed;
            _speedBoostTrail.SetActive(true);
            await Task.Delay(Mathf.RoundToInt(time * 1000f));
            _defaultRunSpeed /= delta;
            _runSpeed = _defaultRunSpeed;
            _speedBoostTrail.SetActive(false);
        }

        private async void GetEffectFromDamage(int damage, Vector2 damageDirection)
        {
            _runSpeed = 0;
            PlayerRb.velocity = Vector3.zero;

            PlayerRb.velocity =
                damageDirection.x <= 0
                ? Vector3.up * 2f - transform.right
                : Vector3.up * 2f + transform.right;

            await Task.Delay(damage * 10);
            _runSpeed = _defaultRunSpeed;
        }
         

#if UNITY_EDITOR
        [ContextMenu("Default Values")]
        public void ResetValues()
        {

        }
#endif
    }
}
