using UnityEngine;

namespace WeaponBehaviour
{
    public class WeaponSway : RootPositioning
    {
        [Header("Input")]
        
        [Header("Position")]
        [SerializeField] private float amount = 0.02f;
        [SerializeField] private float maxAmount = 0.06f;
        [SerializeField] private float smoothAmount = 6f;

        [Header("Rotation")]
        [SerializeField] private float rotationAmount = 4f;
        [SerializeField] private float maxRotationAmount = 5f;

        [SerializeField] private float smoothRotation = 12f;


        [Header("AxisChecked")]
        [SerializeField] private bool AxisX = true;
        [SerializeField] private bool AxisY = true;
        [SerializeField] private bool AxisZ = true;

        [SerializeField] private float zAxisMultiplier = 5f;

        private Vector2 mouseInputs;

        private void Update()
        {
            TiltSway();
            MoveSway();
        }

        public void OnMouseInput(Vector2 newMouseVector)
        {
            mouseInputs = newMouseVector;
        }

        private void MoveSway()
        {
            float moveX;
            float moveY;

            moveX = Mathf.Clamp(mouseInputs.x * amount, -maxAmount, maxAmount);
            moveY = Mathf.Clamp(mouseInputs.y * amount, -maxAmount, maxAmount);
            
            Vector3 finalPosition = new Vector3(moveX, moveY, 0);

            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + rootPosition, Time.deltaTime * smoothAmount);
        }

        private void TiltSway()
        {
            RotationSway(rotationAmount, rotationAmount);
        }

        private void RotationSway(float rotationAmount, float maxRotationAmount)
        {
            float tiltX = Mathf.Clamp(mouseInputs.x * rotationAmount, -maxRotationAmount, maxRotationAmount);

            float tiltY = Mathf.Clamp(mouseInputs.y * rotationAmount, -maxRotationAmount, maxRotationAmount);

            Quaternion finalRotation = Quaternion.Euler(new Vector3(AxisX ? tiltY : 0,
                                                                    AxisY ? tiltX : 0,
                                                                    AxisZ ? tiltX * 2 : 0));

            transform.localRotation = Quaternion.Slerp(transform.localRotation,
                                                       finalRotation * rootRotation,
                                                       Time.deltaTime * smoothRotation);
        }
    }
}