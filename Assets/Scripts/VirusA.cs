using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusA : Desease {

    private void Start()
    {
        virusId = 0;

    onSetTime = 3;

    timeUntilDeath = 30;

    stunTime = 0;

    timeToStun = 0;

    movementPercent = 0.1f;

    probabilityToStun = 0;

}
}
