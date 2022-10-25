using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    // Smooth folowing camera
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float lookAhead;
    private void Update()
    {
        // Classic Mario Bros resctricted no-go-back camera
        if(Mathf.Abs(transform.position.x - player.position.x) < aheadDistance)
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x + aheadDistance, 1.6f, 200f), transform.position.y, transform.position.z);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 1.6f, 200f), transform.position.y, transform.position.z);
        
        // Smooth folowing camera
        //transform.position = new Vector3(palyer.position.x + lookAhead, transform.position.y, transform.position.z);
        //lookAhead = Mathf.Lerp(lookAhead, aheadDistance * player.localScale.x, Time.deltaTime * cameraSpeed);
    }
}
