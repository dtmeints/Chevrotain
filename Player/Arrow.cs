using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    Animator anim;
    private List<string> parentParameters = new List<string>();

    
    
    public AttackCollider chargeAttackScript;

    private ArrowHolder arrowHolder;
     

    private void Awake()
    {
        anim = GetComponent<Animator>();
        arrowHolder = transform.parent.GetComponent<ArrowHolder>();
        AnimatorControllerParameter[] arrowAnimParameters = anim.parameters;
        foreach(var parameter in arrowAnimParameters)
        {
                parentParameters.Add(parameter.name);
        }
    }

    private void Update()
    {
        foreach (var parameter in parentParameters)
        {
            if (playerAnim.GetBool(parameter))
                anim.SetBool(parameter, true);
            else if (!playerAnim.GetBool(parameter))
                anim.SetBool(parameter, false);
        }
    }

    public void SetDamageModifier(float amtToAdd)
    {
        chargeAttackScript.damageModifier += amtToAdd;
    }
    public void ResetDamageModifier()
    {
        chargeAttackScript.damageModifier = 1;
    }

    public void InitiateLeverCrank()
    {
        if (!arrowHolder.currentLever.isFlipped) arrowHolder.currentLever.CrankLever();
        else if (arrowHolder.currentLever.isFlipped) arrowHolder.currentLever.UncrankLever();
    }
    public void EndCrank()
    {
        playerAnim.SetBool("isCranking", false);
        playerAnim.SetBool("isArrowSeparate", false);
        arrowHolder.currentLever.KillLever();
    }

    public void InitiateLeverUncrank()
    {
        arrowHolder.currentLever.UncrankLever();
    }
    public void EndUncrank()
    {
        playerAnim.SetBool("isUncranking", false);
        playerAnim.SetBool("isArrowSeparate", false);
        arrowHolder.currentLever.KillLever();
    }

}
