/// warp to another scene upon collision with a gameObject tagged "player"
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Collider))]
public class WarpPoint : MonoBehaviour
{
    [SerializeField] string sceneName = null;

    void OnTriggerEnter(Collider collider)
    {
        if( collider.tag == "player" )
        {
            SceneManager.LoadScene( sceneName );
        }
    }
    
}