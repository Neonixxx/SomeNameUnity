using SomeName.Core.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SomeName.Core.Items.Impl
{
    public class SimpleGloves : Gloves
    {
        public SimpleGloves()
        {
            Description = "Кожанные перчатки";
            ImageId = "SimpleGloves";
        }
    }
}
