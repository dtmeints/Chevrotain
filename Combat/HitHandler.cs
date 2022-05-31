using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HitHandler
{
    public static void HandleKnockback(GameObject other, IKnockable knockable, Transform player, float knockback)
    {
        float knockbackVectorY = other.transform.position.y - player.position.y;
        float knockbackVectorX = other.transform.position.x - player.position.x;
        float signOfY = Mathf.Sign(knockbackVectorY);
        float signOfX = Mathf.Sign(knockbackVectorX);
        if (Mathf.Abs(knockbackVectorY) > Mathf.Abs(knockbackVectorX))
        {
            knockbackVectorX /= knockbackVectorY;
            knockbackVectorY = signOfY * knockback;
        }
        else if (Mathf.Abs(knockbackVectorX) > Mathf.Abs(knockbackVectorY))
        {
            knockbackVectorY /= knockbackVectorX;
            knockbackVectorX = signOfX * knockback;
        }
        else
        {
            knockbackVectorX = signOfX * knockback;
            knockbackVectorY = signOfY * knockback;
        }
        Vector2 knockbackForce = new Vector2(knockbackVectorX, knockbackVectorY);
        knockable.TakeKnockback(knockbackForce);
    }
}
