using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDeath : Desease {
    private void Start()
    {
        virusId = 2;

        onSetTime = 10;

        timeUntilDeath = 30;

        stunTime = 3;

        timeToStun = 10;

        movementPercent = 0.1f;

        probabilityToStun = 0.15f;

    }
}
