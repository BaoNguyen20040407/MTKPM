using System;
using TapHoa.Models;

namespace TapHoa.Singleton
{
    public sealed class DbSingleton
    {
        private static readonly Lazy<TapHoaEntities> _instance =
            new Lazy<TapHoaEntities>(() => new TapHoaEntities());

        private DbSingleton() { }

        public static TapHoaEntities Instance => _instance.Value;
    }
}
