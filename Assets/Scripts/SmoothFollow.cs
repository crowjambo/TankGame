using UnityEngine;


namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{
		[SerializeField]
		private Transform target;
		[SerializeField]
		private float distance = 3.79f;
		[SerializeField]
		private float height = 2.3f;
		[SerializeField]
		private float rotationDamping = 2.93f;
		[SerializeField]
		private float heightDamping = 2.98f;

		void Start() {
            if (!target)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }

        }
    
		void LateUpdate()
		{
			if (!target)
				return;

			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

			// Always look at the target
			transform.LookAt(target);
		}
	}
}