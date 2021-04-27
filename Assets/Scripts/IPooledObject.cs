using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Redux
{
    public interface IPooledObject
    {
        void OnObjectSpawn();
    }
}