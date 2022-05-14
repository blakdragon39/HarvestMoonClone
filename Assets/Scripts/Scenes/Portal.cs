using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField] private int sceneToLoad;
    [SerializeField] private string portalId; 
    [SerializeField] private string destPortalId; 
    [SerializeField] private Vector2 spawnDirection;
}