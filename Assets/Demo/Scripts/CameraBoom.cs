using UnityEngine;

namespace Demo.Scripts
{
    public class CameraBoom : MonoBehaviour
    {
        [SerializeField] private RectTransform viewportArea;
        
        [SerializeField] private float sensitivity = 1f;
        [SerializeField] private float maxLength = 5f;
        [SerializeField] private float minLength = 1f;
        
        [SerializeField, Range(0, 90f)] private float maxPitch = 90f;
        [SerializeField, Range(-90f, 0f)] private float minPitch = -90f;

        private Camera _camera;
        private float _pitch = 0f;
        private bool _isRotating;

        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void UpdateRotation()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _isRotating = !_isRotating;
                Cursor.visible = !_isRotating;
                Cursor.lockState = _isRotating ? CursorLockMode.Locked : CursorLockMode.Confined;
            }
            
            if (!_isRotating)
            {
                return;
            }
            
            float horizontal = Input.GetAxis("Mouse X");
            float vertical = Input.GetAxis("Mouse Y");

            _pitch = Mathf.Clamp(_pitch - vertical * sensitivity, minPitch, maxPitch);
            
            transform.rotation *= Quaternion.Euler(0f, horizontal * sensitivity, 0f);
            Vector3 euler = transform.rotation.eulerAngles;
            euler.x = _pitch;
            euler.z = 0f;
            transform.rotation = Quaternion.Euler(euler);
        }

        private void UpdateBoomLength()
        {
            float wheelValue = Input.GetAxis("Mouse ScrollWheel");
            Vector3 cameraPosition = _camera.transform.localPosition;
            cameraPosition.z = Mathf.Clamp(cameraPosition.z + wheelValue, -maxLength, -minLength);
            _camera.transform.localPosition = cameraPosition;
        }

        private void Update()
        {
            UpdateRotation();
            UpdateBoomLength();
        }
    }
}