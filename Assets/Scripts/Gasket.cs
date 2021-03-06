using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gasket : MonoBehaviour {
	public GameObject prefab;
	public float spacing = 5f;
	public int size = 3;

	private List<GameObject> copies = new List<GameObject>();
	private List<Vector3> locs = new List<Vector3>();
  private Quaternion defaultRotation;
	private float twoThirds = 2f/3f;
	private bool inPlace = true;
	private bool interruptReset = false;
  private bool resetting = false;


	void Start() {
    defaultRotation = prefab.transform.rotation;
		GaskIt(new Vector3(-spacing, 0f, -0.577357f * spacing) + transform.position, 
					 new Vector3(spacing, 0f, -0.577357f * spacing) + transform.position,
					 new Vector3(0f, 0f, 1.1547f * spacing) + transform.position,
					 new Vector3(0f, 1.7320508074f * spacing, 0f) + transform.position,
					 size, 
           1f);
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.E))
			Explode();
    if(Input.GetKeyDown(KeyCode.R))
      Reset();
	}

	public void Explode() {
		inPlace = false;
		interruptReset = true;
		Vector3 explosionPosition = new Vector3(0f, 
																						0.8660254037f * spacing, 
																						0f) + transform.position;
		for(int i = 0; i < copies.Count; i++) {
			Rigidbody rb = copies[i].GetComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.None;
			rb.AddExplosionForce(Random.Range(600f, 1000f), // force
													 explosionPosition,         // position
													 1000f,											// radius 
													 5f); 											// mode
		}
	}

  public void Reset() {
    if(!resetting && !inPlace) {
      resetting = true;
      interruptReset = false;
      for(int z = 0; z < locs.Count; z++){
        StartCoroutine(MoveObject(copies[z], locs[z], 1.2f));
        //copies[z].transform.rotation = Quaternion.identity;
        //copies[z].rigidbody.constraints = RigidbodyConstraints.FreezeAll;
      }
    }
  }

  IEnumerator MoveObject(GameObject gameObject, Vector3 endPosition, float time) {
    float rate = 1f / time;
    Vector3 startPosition = gameObject.transform.position;
    Quaternion startRotation = gameObject.transform.rotation;

    float i = 0f;
    while (i < 1f) {
      if(interruptReset) {
        resetting = false;
        yield break;
      }
      i += Time.deltaTime * rate;
      gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, i);
      gameObject.transform.rotation = Quaternion.Slerp(startRotation, defaultRotation, i);
      yield return 0;
    }

    gameObject.transform.position = endPosition;
    gameObject.transform.rotation = defaultRotation;
    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

    resetting = false;
    inPlace = true;
  }

	private void GaskIt(Vector3 a, Vector3 b, Vector3 c, Vector3 d, 
			int n, float scale) {
		if(n > 0) {
			AddIfNotAlreadyPresent(a); AddIfNotAlreadyPresent(b);
			AddIfNotAlreadyPresent(c); AddIfNotAlreadyPresent(d);

			// find midpoints
			Vector3 e = Vector3.Lerp(a, b, 0.5f); Vector3 f = Vector3.Lerp(a, c, 0.5f);
			Vector3 g = Vector3.Lerp(a, d, 0.5f); Vector3 h = Vector3.Lerp(b, c, 0.5f);
			Vector3 i = Vector3.Lerp(b, d, 0.5f); Vector3 j = Vector3.Lerp(c, d, 0.5f);

			// recurse
			GaskIt(a, g, f, e, n - 1, scale * twoThirds);
			GaskIt(e, h, b, i, n - 1, scale * twoThirds);
			GaskIt(c, h, f, j, n - 1, scale * twoThirds);
			GaskIt(d, g, i, j, n - 1, scale * twoThirds);
		}
	}

	private void AddIfNotAlreadyPresent(Vector3 loc) {
		if(!locs.Contains(loc)) {
			locs.Add(loc);
			GameObject copy = Instantiate(prefab, loc, defaultRotation) as GameObject;
			copy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
			copies.Add(copy);
		}
	}

}