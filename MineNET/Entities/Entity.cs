using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineNET.Entities
{
    public abstract class Entity
    {
        private static Dictionary<int, Type> entityRegistory;

        public static void RegisterEntity(int id, Type type)
        {
            entityRegistory.Add(id, type);
        }

        public Entity()
        {

        }
    }
}
