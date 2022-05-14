using UnityEngine;

public class EssentialObjectsSpawner : MonoBehaviour {

    [SerializeField] private GameObject essentialObjectsPrefab;

    private void Awake() {
        var existingObject = FindObjectOfType<EssentialObjects>();
        if (existingObject == null) {
            Instantiate(essentialObjectsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}