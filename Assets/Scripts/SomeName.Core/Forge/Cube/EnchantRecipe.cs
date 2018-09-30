using System;
using System.Collections.Generic;
using SomeName.Core.Domain;
using SomeName.Core.Items.Interfaces;

namespace SomeName.Core.Forge.Cube
{
    public class EnchantRecipe : BaseRecipe
    {
        public EnchantManager EnchantManager { get; set; } = EnchantManager.Standard;

        private readonly Type[] _requiredTypes = new Type[] { typeof(ScrollOfEnchant), typeof(ICanBeEnchanted) };

        public EnchantRecipe(List<IItem> cube) : base(cube) { }

        public override double GetChance()
        {
            return EnchantManager.GetEnchantChance(Get<ICanBeEnchanted>(), Get<ScrollOfEnchant>());
        }

        public override void Transform()
        {
            var iCanBeEnchanted = Get<ICanBeEnchanted>();
            var scrollOfEnchant = Get<ScrollOfEnchant>();
            var enchantResult = EnchantManager.TryEnchant(iCanBeEnchanted, scrollOfEnchant);
            Cube.Remove(scrollOfEnchant);
            if (!enchantResult)
                EnchantManager.SetEnchantmentLevel(iCanBeEnchanted, 0);
        }

        public override bool Validate()
        {
            if (!ContainsOnly(_requiredTypes))
                return false;

            return EnchantManager.CanBeEnchantedWith(Get<ICanBeEnchanted>(), Get<ScrollOfEnchant>());
        }
    }
}
