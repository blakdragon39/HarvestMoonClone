using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    
    // public Vector2 MaxBoundary { get; set; }
    // public Vector2 MinBoundary { get; set; }

    private void LateUpdate() {
        if (transform.position != target.position) {
            var targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            // targetPos.x = Mathf.Clamp(targetPos.x, MinBoundary.x, MaxBoundary.x);
            // targetPos.y = Mathf.Clamp(targetPos.y, MinBoundary.y, MaxBoundary.y);
            
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}
