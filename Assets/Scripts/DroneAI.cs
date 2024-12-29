using UnityEngine;
using UnityEngine.AI;
public class DroneAI : MonoBehaviour
{

    public NavMeshAgent agent; // Drone'un NavMesh ajanı
    public float speed = 2f; // Drone'un uçma hızı
    public GameObject targetObject; // Drone'un hedef noktası
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = speed;
            agent.acceleration = 20f; // Hızlı hareket için yüksek ivme
        }

    }

    public void SetTarget(GameObject targetPosition)
    {
        targetObject = targetPosition;
        if (agent != null)
        {
            agent.SetDestination(targetObject.transform.position);
        }
    }
    void OnDrawGizmos()
    {
        if (agent != null && agent.path != null)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
            }
        }
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, targetObject.transform.position) < 5f)
        {
            Destroy(gameObject);
        }
    }
}


