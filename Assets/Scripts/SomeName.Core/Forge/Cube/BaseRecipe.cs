using System;
using System.Collections.Generic;
using System.Linq;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Forge.Cube
{
    public abstract class BaseRecipe
    {
        protected List<IItem> Cube;

        protected BaseRecipe(List<IItem> cube)
        {
            Cube = cube;
        }

        public abstract bool Validate();

        public abstract void Transform();

        public abstract double GetChance();

        protected bool ContainsOnly(params Type[] types)
        {
            if (Cube.Count != types.Length)
                return false;
            var cubeTypes = Cube.Select(s => s.GetType()).ToArray();
            var temp = types.ToList();
            for (int i = 0; i < types.Length; i++)
            {
                var isFinded = false;
                foreach (var item in temp)
                {
                    if (item.IsAssignableFrom(cubeTypes[i]))
                    {
                        temp.Remove(item);
                        isFinded = true;
                        break;
                    }
                }
                if (!isFinded)
                    return false;
            }
            return true;
        }

        protected T Get<T>()
            where T : IItem
            => (T)Cube.First(s => s is T);
    }
}
