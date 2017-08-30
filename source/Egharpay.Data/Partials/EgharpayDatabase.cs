using System.Data.Entity;
using Egharpay.Entity;

namespace Egharpay.Data
{
    public partial class EgharpayDatabase : DbContext
    {
        public EgharpayDatabase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Initialise();
        }

        private void Initialise()
        {
            //Disable initializer
            Database.SetInitializer<EgharpayDatabase>(null);
            Database.CommandTimeout = 300;
            Configuration.ProxyCreationEnabled = false;
        }
    }
}
