using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Domain
{
    public interface IInventoryBag
    {
        int Count { get; }

        IItem Get(int index);
    }
}
