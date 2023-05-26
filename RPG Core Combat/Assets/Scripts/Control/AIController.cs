using UnityEngine;
using RPG.Combat;
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        Fighter fighter;
        GameObject player;
        
        [SerializeField] float chaseDistance = 1f;
        private void Start() {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }
        private void Update()
        {
            if(IsInDistanceToPlayer() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool IsInDistanceToPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position); 
            return distanceToPlayer < chaseDistance;

        }
    }
}
