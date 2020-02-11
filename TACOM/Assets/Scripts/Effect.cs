using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//represents any event that modifies a characters attributes
public class Effect
{
    string att;
    int value;

    public Effect()
    {
        att = "health";
        value = 0;
    }

    public Effect(string att,int value)
    {
        this.att = att;
        this.value = value;
    }

    public void doEffect(Character target)
    {
        Debug.Log("do");
        target.handleEffect(att, value);
    }
}
