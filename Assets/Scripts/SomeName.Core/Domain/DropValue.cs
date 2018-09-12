using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Convert;

namespace SomeName.Core.Domain
{
    public class DropValue
    {
        public long Exp { get; set; }

        public long Gold { get; set; }

        public long Items { get; set; }

        public DropValue ApplyKoef(double koef)
            => ApplyKoefs(koef, koef, koef);

        public DropValue ApplyKoefs(double expKoef = 1.0, double goldKoef = 1.0, double itemsKoef = 1.0)
        {
            Exp = ToInt64(Exp * expKoef);
            Gold = ToInt64(Gold * goldKoef);
            Items = ToInt64(Items * itemsKoef);
            return this;
        }
    }
}
