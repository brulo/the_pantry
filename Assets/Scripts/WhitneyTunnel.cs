/* using UnityEngine; */
/* using System.Collections; */
/* using System.Collections.Generic; */

/* public class WhitneyTunnel { */
/* 	public GameObject prefab; */
/* 	public float rate = 1f; */
/* 	public int numCopies; */
/* 	public float spacing; */

/* 	private GameObject[] copies; */
/* 	private float degrees = 0f; */
/* 	private float rads = 0f; */

/* 	void Start() { */
/* 		copies = new GameObject[numCopies]; */
/* 		for(int i = 0; i < copies.Length; i++) { */
/* 			GameObject copy = Instantiate(prefab, */ 
/* 																		new Vector3(i - 5, 1, 0), */
/* 																		Quaternion.identity) as GameObject; */
/* 			copy.transform.Rotate(0, 0, 180); */
/* 			HSBColor color = new HSBColor((i * 1f) / copies.Length, 1f, 1f); */
/* 			// set color without send message? */
/* 			copies[i] = copy; */
/* 		} */
/* 	} */

/* 	void Update() { */
/* 		degrees += (Time.deltaTime * rate); */
/* 		degrees = degrees % 360f; */
/* 		rads = degrees * (Mathf.PI / 180f); */

/* 		for (int i = copies.Length - 1; i >= 0; i--) { */
/* 			if(i < numCopies) { */
/* 				float x = Mathf.Sin(rads * (i + 1)) * spacing; */
/* 				float y = Mathf.Cos(rads * (i + 1)) * spacing; */
/* 				float z = numCopies - i; */
/* 				copies[i].transform.position = new Vector3(x, y, z); */

/* 				float zRotate = -(Time.deltaTime * rate * (i + 1)); */
/* 				copies[i].transform.Rotate(0, 0, zRotate); */
/* 			} */
/* 		} */
/* 	} */
/* } */
