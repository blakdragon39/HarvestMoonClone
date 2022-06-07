using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    private static bool sceneSwitching;
    
    [SerializeField] private int sceneToLoad;
    [SerializeField] private string portalId; 
    [SerializeField] private string destPortalId;
    [SerializeField] private Vector2 spawnDirection;
    
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
        
        // todo pause input?
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        // todo door animation and fade

        var destPortal = FindObjectsOfType<Portal>()
            .First(portal => portal.portalId == destPortalId);

        yield return player.MoveToPosAndWalk(destPortal.gameObject.transform.position, spawnDirection);
        
        // todo fade back in
        
        // todo unpause input?
        Destroy(gameObject);
        sceneSwitching = false;
    }
}
