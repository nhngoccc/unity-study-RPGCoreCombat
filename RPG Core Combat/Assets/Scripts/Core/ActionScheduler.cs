using UnityEngine;
using RPG.Core;
namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IActions currentAction;
        public void StartAction(IActions action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
                //  Debug.Log("Cancelling Action" + currentAction);
            }
            currentAction = action;

        }
        public void CancelCurrentAction()
        {
            StartAction(null);
        }

    }
}

