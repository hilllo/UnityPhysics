using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Physics
{
    [DisallowMultipleComponent]
    public class PhysicsEngine : MonoBehaviour
    {
        #region Fields

        [SerializeField, Tooltip("Average velocity (distance per sec)")]
        private Vector3 _Velocity;

        private Vector3 _NetForce;

        public List<Vector3> ForceList;

        #endregion Fields

        #region MonoBehaviours

        void Start()
        {
            this._NetForce = new Vector3(0.0f, 0.0f, 0.0f);
            if (this.ForceList == null)
                this.ForceList = new List<Vector3>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            this.CalNetForces();

            if(this._NetForce == Vector3.zero)
                this.transform.position += this._Velocity * Time.deltaTime;
            // TODO: if there're net force
        }

        #endregion MonoBehaviours

        private void CalNetForces()
        {
            this._NetForce = Vector3.zero;

            foreach(Vector3 force in this.ForceList)
            {
                this._NetForce += force;
            }
        }
    }
}
