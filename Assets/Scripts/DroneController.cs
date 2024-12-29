using UnityEngine;
using UnityEngine.AI;

public class DroneController : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform'u
    public GameObject exit; // Maze çıkışı
    public GameObject dronePrefab; // Drone prefab
    private GameObject activeDrone; // Mevcut aktif drone
    public float idleTime = 5f; // Oyuncunun hareketsiz kalma süresi
    private Vector3 lastPlayerPosition; // Oyuncunun son bilinen pozisyonu
    private float idleTimer = 0f;
    void Start()
    {
        lastPlayerPosition = player.position; // Oyuncunun ilk pozisyonunu kaydet
    }

    void Update()
    {
        // Oyuncunun hareket edip etmediğini kontrol et
        if (Vector3.Distance(lastPlayerPosition, player.position) < 0.1f)
        {
            idleTimer += Time.deltaTime;

            // Oyuncu hareketsizse ve süre dolmuşsa
            if (idleTimer >= idleTime && activeDrone == null)
            {
                SpawnAndGuideDrone();
            }
        }
        else
        {
            idleTimer = 0f; // Oyuncu hareket ettiğinde zamanlayıcı sıfırlanır
        }

        lastPlayerPosition = player.position; // Oyuncunun pozisyonunu güncelle
    }

    void SpawnAndGuideDrone()
    {
        Vector3 spawnPosition = player.position + new Vector3(0, 5, 0); // Havada bir pozisyon belirle

        GameObject drone = Instantiate(dronePrefab, spawnPosition, Quaternion.identity);
        DroneAI droneAI = drone.GetComponent<DroneAI>();
        activeDrone = drone;
        if (droneAI != null)
        {
            droneAI.SetTarget(exit);
            drone.GetComponent<NavMeshAgent>().SetDestination(exit.transform.position); // Drone'un hedefini ayarla
        }
    }
}

