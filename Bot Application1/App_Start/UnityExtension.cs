using Bot_Application1.Storage;
using Unity;
using Unity.Extension;

namespace Bot_Application1.App_Start
{
    public class UnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IStorageManager, StorageManager>();
            Container.RegisterType<IDataAccess, DataAccess>();
        }
    }
}