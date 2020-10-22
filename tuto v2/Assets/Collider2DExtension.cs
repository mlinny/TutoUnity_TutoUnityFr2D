using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public static class Collider2DExtension
    {

        public static bool IsInCollisionWithPlayer(Collider2D collision)
        {
            return collision.CompareTag("Player");
        }

    }
}
