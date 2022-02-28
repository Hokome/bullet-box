using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletBox
{
    public interface IHittable
    {
		void Hit(float damage);
    }
}
