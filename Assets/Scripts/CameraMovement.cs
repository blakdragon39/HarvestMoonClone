using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    [SerializeField] private Vector2 maxBoundary;
    [SerializeField] private Vector2 minBoundary;

    private void LateUpdate() {
        if (transform.position != target.position) {
            var targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPos.x = Mathf.Clamp(targetPos.x, minBoundary.x, maxBoundary.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minBoundary.y, maxBoundary.y);
            
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}
