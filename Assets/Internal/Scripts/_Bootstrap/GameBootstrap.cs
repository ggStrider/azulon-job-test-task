using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Core.Data.Inventory;
using UnityEngine;

namespace Internal.Scripts._Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            var inventory = CreateInventory();
            ServiceLocator.Register<IInventory>(to: inventory);
        }

        private IInventory CreateInventory()
        {
            return new PlayerInventory();
        }
    }
}