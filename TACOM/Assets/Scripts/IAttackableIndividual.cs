using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackableIndividual : IAttackable
{
    int GetRangedDefense();
    int GetArmor();
}
