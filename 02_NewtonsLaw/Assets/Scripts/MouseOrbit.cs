using UnityEngine;
using System.Collections;
using System;

namespace Game.Physics
{
    [DisallowMultipleComponent]
    public class MouseOrbit : MonoBehaviour
    {
        [SerializeField]
        private Transform _Target;

        [SerializeField]
        private float _Distance = 10.0f;

        [SerializeField]
        private float _XSpeed = 250.0f;
        [SerializeField]
        private float _YSpeed = 120.0f;

        [SerializeField]
        private float _YMinLimit = -20.0f;
        [SerializeField]
        private float _YMaxLimit = 80.0f;

        private float _X = 0.0f;
        private float _Y = 0.0f;

        void Start()
        {
            Vector3 angles = this.transform.eulerAngles;
            this._X = angles.y;
            this._Y = angles.x;

            // Make the rigid body not change rotation
            if (this.GetComponent<Rigidbody>())
                this.GetComponent<Rigidbody>().freezeRotation = true;
        }

        void LateUpdate()
        {
            if (this._Target == null)
                return;

            this._X += Input.GetAxis("Mouse X") * this._XSpeed * 0.02f;
            this._Y += Input.GetAxis("Mouse Y") * this._XSpeed * 0.02f;
            this._Distance += Input.mouseScrollDelta.y * 0.2f;

            this._Y = ClampAngle(this._Y, this._YMinLimit, this._YMaxLimit);

            Quaternion rotation = Quaternion.Euler(this._Y, this._X, 0.0f);
            Vector3 position = rotation * (new Vector3(0.0f, 0.0f, -this._Distance)) + this._Target.position;

            this.transform.rotation = rotation;
            this.transform.position = position;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360.0f)
                angle += 360.0f;
            if (angle > 360.0f)
                angle -= 360.0f;

            return Mathf.Clamp(angle, min, max);
        }
    }
}
