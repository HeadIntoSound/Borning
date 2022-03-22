using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class randomGenerator
{
    public static int randomInt(int min,int max,string salt)
    {
        byte[] saltByte = System.Text.Encoding.UTF8.GetBytes(salt);
        int saltInt = Mathf.Abs(System.BitConverter.ToInt32(saltByte,0));
        int number = Random.Range(min,max);
        number = Mathf.Abs(((number * saltInt) * Random.Range(min,saltInt)) % max);
        return number;
    }
}
