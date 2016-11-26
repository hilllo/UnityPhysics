using UnityEngine;
using System.Collections;

namespace Game.Physics
{
    [RequireComponent(typeof(PhysicsEngine))]
    public class AddForce : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _Force;

        private PhysicsEngine _PhysicsEngine;

        void Start()
        {
            this._PhysicsEngine = this.GetComponent<PhysicsEngine>();
        }

        private void FixedUpdate()
        {
            this._PhysicsEngine.AddForce(this._Force);
        }
    }
}
