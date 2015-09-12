using UnityEngine;
using System.Collections; 
using System.Collections.Generic; 

public class WhitneyTunnel : MonoBehaviour { 
  public GameObject prefab; 
  public float rate; 
  public int numberOfCopies; 
  public float spacing; 

  private GameObject[] copies; 
  private Light[] lights;
  private float degrees = 0f; 
  private float rads = 0f; 


  void Start() { 
    copies = new GameObject[numberOfCopies]; 
    lights = new Light[numberOfCopies];
    for(int i = 0; i < copies.Length; i++) { 
      GameObject copy = Instantiate(prefab,  
                                    transform.position, 
                                    Quaternion.identity) as GameObject; 
      copies[i] = copy; 
      lights[i] = copy.transform.Find("Light").GetComponent<Light>();
    } 
  } 

  void Update() { 
    degrees += (Time.deltaTime * rate); 
    degrees = degrees % 360f; 
    rads = degrees * (Mathf.PI / 180f); 

    for (int i = copies.Length - 1; i > -1; i--) { 
      if(i < numberOfCopies) { 
        float x = Mathf.Sin(rads * (i + 1)) * spacing; 
        float y = Mathf.Cos(rads * (i + 1)) * spacing; 
        float z = numberOfCopies - i; 
        copies[i].transform.position = new Vector3(x, y, z) + transform.position; 

        float zRotate = -1 * (Time.deltaTime * rate * (i + 1)); 
        copies[i].transform.Rotate(0, 0, zRotate); 

        HSBColor color = new HSBColor((i * 1f) / copies.Length, 1f, 1f);
        lights[i].color = color.ToColor();
      } 
    } 
  } 

} 