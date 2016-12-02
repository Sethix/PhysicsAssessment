using UnityEngine;
using System.Collections;

public class ArmorAccessory : CoreAccessory
{

    private float extraHits = 1;

    public override void HitFunction()
    {
        if (extraHits > 0)
        {
            base.HitFunction();
            replaceHit = true;
            extraHits--;
        }
        else
            Destroy(this);
    }

}
