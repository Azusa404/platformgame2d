using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DelayScript {
    public static IEnumerator run(Action action,float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}
