using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusS : Desease {

    private void Start()
    {
        virusId = 1;

        onSetTime = 5;

        timeUntilDeath = 20;

        stunTime = 2;

        timeToStun = 10;

        movementPercent = 0.2f;

        probabilityToStun = 0.05f;

    }
}
