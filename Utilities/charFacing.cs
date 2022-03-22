using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class charFacing
{

    public static int facing(Vector3 direction, float gap)
    {
        int f = 0;

        if (direction.x >= -gap && direction.x <= gap && direction.y > 0)
        {
            f = 4;
            // faces N
        }

        if (direction.x > -gap && direction.x < gap && direction.y < 0)
        {
            f = 0;
            // faces S
        }

        if (direction.x > 0 && direction.y >= -gap && direction.y <= gap)
        {
            f = 6;
            // faces E
        }

        if (direction.x < 0 && direction.y > -gap && direction.y < gap)
        {
            f = 2;
            // faces W
        }

        if (direction.x > gap && direction.y > gap)
        {
            f = 5;
            //faces NE
        }

        if (direction.x < -gap && direction.y > gap)
        {
            f = 3;
            //faces NW
        }

        if (direction.x < -gap && direction.y < -gap)
        {
            f = 1;
            //faces SW
        }

        if (direction.x > gap && direction.y < -gap)
        {
            f = 7;
            //facing SE
        }

        return f;
    }

}
