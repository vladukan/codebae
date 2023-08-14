using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.Input;
using Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Player
{
    public class PlayerMove : MonoBehaviour, ISavedProgress
    {
        public CharacterController CharacterController;
        public float MovementSpeed;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            CharacterController.Move(movementVector * MovementSpeed * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel());

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (progress.WorldData.PositionOnLevel.Position != null)
                    Warp(to: savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            CharacterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(CharacterController.height);
            CharacterController.enabled = true;
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}