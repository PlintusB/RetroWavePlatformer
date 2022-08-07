using System.Threading.Tasks;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private UserInput _inputs;
        [SerializeField] private PlayerMovement _playerMov;
        [SerializeField] private PlayerAnimationController _playerAnim;
        [SerializeField] private GroundChecker _ground;
        [SerializeField] private GameObject _immortalSphere;

        public int XMoveInput { get; private set; }
        public int YMoveInput { get; private set; }
        public bool IsJumpButtonPressed { get; private set; }

        public float VerticalVelocity { get; private set; }
        public bool IsGrounded { get; private set; }
        public bool IsCanControl { get; private set; }
        public bool IsDead { get; private set; }

        void Awake()
        {
            IsCanControl = true;
            IsDead = false;
            EventManager.OnImmortalStatusChanged.AddListener(GetEffectFromImmortalBonus);
            _immortalSphere.SetActive(false);
            EventManager.OnDamageReceived.AddListener(ParalizeAfterDamage);
            EventManager.OnDamageReceived.AddListener(ImmortalStateAfterDamage);
            EventManager.OnPlayerDied.AddListener(ActivationEndLevelState);
            EventManager.OnLevelWinEnded.AddListener(ActivationEndLevelState);
        }

        void Update()
        {
            IsGrounded = _ground.IsPlayerGrounded;

            if (!IsCanControl) return;
            XMoveInput = _inputs.HorizontalAxis;
            YMoveInput = _inputs.VerticalAxis;
            IsJumpButtonPressed = _inputs.IsJumpButtonPressed;
            VerticalVelocity = _playerMov.PlayerRb.velocity.y;
        }

        private async void GetEffectFromImmortalBonus(float time)
        {            
            gameObject.layer = 9;
            _immortalSphere.SetActive(true);
            await Task.Delay(Mathf.RoundToInt(time * 1000f));
            gameObject.layer = 6;
            _immortalSphere.SetActive(false);
        }

        private async void ParalizeAfterDamage(int damage, Vector2 dir)
        {            
            IsCanControl = false;
            await Task.Delay(damage * 10);
            if (IsDead) return;
            IsCanControl = true;            
        }

        private async void ImmortalStateAfterDamage(int damage, Vector2 dir)
        {
            gameObject.layer = 9;
            await Task.Delay(damage * 2 * 10);
            if (IsDead) return;
            gameObject.layer = 6;
        }

        private void ActivationEndLevelState()
        {
            IsDead = true;
            IsCanControl = false;
            XMoveInput = 0;
            gameObject.layer = 9;
        }
    }
}