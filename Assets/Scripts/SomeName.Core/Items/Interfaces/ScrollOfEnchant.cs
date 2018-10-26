using static System.Environment;

namespace SomeName.Core.Items.Interfaces
{
    public abstract class ScrollOfEnchant : Item
    {
        public ScrollOfEnchant()
        {
            CanStack = true;
        }
    }
}
