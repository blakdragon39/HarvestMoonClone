using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    private static bool sceneSwitching;
    
    [SerializeField] private int sceneToLoad;
    [SerializeField] private string portalId; 
    [SerializeField] private string destPortalId;
    [SerializeField] private Vector2 spawnDirection;

    [SerializeField] [CanBeNull] private Door door;
    
    private void OnTriggerEnter2D(Collider2D col) {
        if (sceneSwitching) return;
        
        var player = col.gameObject.GetComponent<PlayerController>();

        if (player != null) {
            StartCoroutine(SwitchScene(player));
        }
    }

    private IEnumerator SwitchScene(PlayerController player) {
        sceneSwitching = true;
        DontDestroyOnLoad(gameObject);
        
        if (door != null) {
            yield return door.OpenDoor();    
        }
        
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        // todo fade?

        var destPortal = FindObjectsOfType<Portal>()
            .First(portal => portal.portalId == destPortalId);
        
        yield return player.MoveToPosAndWalk(destPortal.gameObject.transform.position, spawnDirection);
        
        // todo fade back in?
        
        Destroy(gameObject);
        sceneSwitching = false;
    }
}
