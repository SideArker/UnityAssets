using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeAnimatorBehaviour : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(animator.GetParameter(0).defaultInt);
    }
}
