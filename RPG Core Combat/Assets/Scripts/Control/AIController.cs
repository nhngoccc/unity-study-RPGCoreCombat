using UnityEngine;
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 1f;
        private void Update()
        {
            if(DistanceToPlayer() < chaseDistance)
            {
                Debug.Log(gameObject.name + "Come and hit");
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(gameObject.transform.position, transform.position);

        }
    }
}
