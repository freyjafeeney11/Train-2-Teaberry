using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform player;

    [SerializeField] GameObject background;

    [Range(0, 1)]

    [SerializeField] float parallaxScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Parallax();
    }

    void Parallax()
    {
        background.transform.position = new Vector3(-transform.position.x * parallaxScale, - transform.position.y * parallaxScale, background.transform.position.z * parallaxScale);
    }
}
