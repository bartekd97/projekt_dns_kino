using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Wave  {

    public Vector3 startPoint;
    public float range;
    public float startTime;
     virtual public float getPosY(Vector3 position,
        float speed,
        float scale,
        float waveDistance,
        float noiseStrength,
        float noiseWalk,
        float timeSinceStart)
    {
        return 0;

    }

    virtual public bool isFinished(
        float speed,
        float scale,
        float waveDistance,
        float noiseStrength,
        float noiseWalk,
        float timeSinceStart)
    {
        return true;

    }


}
