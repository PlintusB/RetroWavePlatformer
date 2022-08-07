using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerControl
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerMan;
        private Animator _animator;
        private bool _isHurt;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _isHurt = false;
            EventManager.OnDamageReceived.AddListener(ReceiveDamage);

        }

        void Update()
        {
            _animator.SetBool("IsGrounded", _playerMan.IsGrounded);
            _animator.SetInteger("X_move", Mathf.Abs(_playerMan.XMoveInput));
            _animator.SetFloat("Y_move", _playerMan.VerticalVelocity);
            _animator.SetBool("Hurt", _isHurt);

            FlipX();
        }

        void FlipX()
        {
            if (_playerMan.XMoveInput > 0 && transform.localScale.x == -1f)
                transform.localScale = new Vector2(1, 1);
            if (_playerMan.XMoveInput < 0 && transform.localScale.x == 1f)
                transform.localScale = new Vector2(-1, 1);
        }

        private async void ReceiveDamage(int damage, Vector2 dir)
        {
            _isHurt = true;
            await Task.Delay(damage * 10);
            _isHurt = false;
        }
    }
}