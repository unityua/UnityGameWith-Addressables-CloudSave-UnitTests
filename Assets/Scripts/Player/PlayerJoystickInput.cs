using PesPatron.Characters;
using UnityEngine;

namespace PesPatron.PlayerStuff
{
    public class PlayerJoystickInput : MonoBehaviour
    {
        private CharacterMovement _movementTarget;
        private Joystick _joystick;
        private Transform _mainCameraTransform;

        public void Construct(Transform mainCameraTransform, Joystick joystick, CharacterMovement movementTarget)
        {
            _mainCameraTransform = mainCameraTransform;
            _movementTarget = movementTarget;
            _joystick = joystick;
        }

        public void Initialize()
        {
            _movementTarget.BecameEnabled += (m) => _joystick.SetVisible(true);
            _movementTarget.BecameDisabled += (m) => _joystick.SetVisible(false);
        }

        private void Update()
        {
            Vector3 moveDirection = JoystickInputToGroundDirection(GetMoveDirection());
            _movementTarget.SetMoveDirection(moveDirection);
        }

        private Vector2 GetMoveDirection()
        {
            Vector2 joystickInput = _joystick.Direction;

            if (joystickInput != Vector2.zero)
                return joystickInput;

            Vector2 resultInput = Vector2.zero;

            resultInput.x = Input.GetAxis("Horizontal");
            resultInput.y = Input.GetAxis("Vertical");

            if (resultInput.magnitude > 1f)
                resultInput = resultInput.normalized;

            return resultInput;
        }

        private Vector3 JoystickInputToGroundDirection(Vector2 joystickInput)
        {
            float cameraFacing = _mainCameraTransform.eulerAngles.y;
            Vector3 direction = new Vector3(joystickInput.x, 0f, joystickInput.y);
            return Quaternion.Euler(0, cameraFacing, 0) * direction;
        }
    }
}