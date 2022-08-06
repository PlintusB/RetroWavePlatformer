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

        //private bool _isCanControl;

        void Awake()
        {
            // приравнять _isCanControl к аналогичному в ГеймМанагере
            EventManager.OnImmortalStatusChanged.AddListener(GetEffectFromImmortalBonus);
            _immortalSphere.SetActive(false);
        }

        void Start()
        {

        }

        void Update()
        {
            XMoveInput = _inputs.HorizontalAxis;
            YMoveInput = _inputs.VerticalAxis;
            IsJumpButtonPressed = _inputs.IsJumpButtonPressed;
            IsGrounded = _ground.IsPlayerGrounded;
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
    }
}