using SomeName.Core.Items.Interfaces;
using UnityEngine;

namespace SomeName.Core.Items.Impl
{
    public class SimpleSword : Weapon
    {
        public SimpleSword()
        {
            Description = "Стальной меч";
            Image = Resources.Load<Sprite>("SimpleSword.jpg");
        }
    }
}
