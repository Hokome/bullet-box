using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public interface IEnemySpawner
    {
		void Spawn(Spawnable s, Vector3 p, Color c);
    }
}
