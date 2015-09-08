/* using UnityEngine; */
/* using System.Collections; */
/* using System.Collections.Generic; */

/* public class Pyramid : MonoBehaviour { */
/* 	public GameObject prefab; */
/* 	public int baseLength = 3; */
/* 	public float spacing = 5f; */

/* 	private List<GameObject> copies = new List<GameObject>(); */
/* 	private List<Vector3> locs = new List<Vector3>(); */

/* 	void Start() { */
/* 		layerStartPosition = transform.position; */

/* 		while(layerLength > 0) { */
/* 			for(int x = 0; x < layerLength; x++) { */
/* 				for(int z = 0; z < layerLength; z++) { */
/* 					Vector3 loc = new Vector3(x * spacing, 0f, z * spacing) + startPosition; */
/* 					locs.Add(loc); */
/* 					GameObject copy = Instantiate(prefab, loc, Quaternion.identity) as GameObject; */
/* 					copies.Add(copy); */
/* 				} */
/* 			} */				
/* 			newPosition.x += spacing * 0.5f; */
/* 			layerLength--; */
/* 		} */
/* 	} */

/* 			for(int x = 0; x < layerLength; x++) { */
/* 				for(int z = 0; z < layerLength; z++) { */
/* 					Vector3 loc = new Vector3(x * spacing, 0f, z * spacing) + startPosition; */
/* 					locs.Add(loc); */
/* 					GameObject copy = Instantiate(prefab, loc, Quaternion.identity) as GameObject; */
/* 					copies.Add(copy); */
/* 				} */
/* 			} */				
/* 			// recurse */
/* 			Vector3 newPosition = startPosition; */
/* 			newPosition.x += spacing * 0.5f; */
/* 			InitializePyramid(layerLength - 1, spacing, newPosition); */
/* 		} */
/* 	} */

/* } */
