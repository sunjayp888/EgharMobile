using System;
using Egharpay.Data.Interfaces;

namespace Egharpay.Data.Models
{
    public class EgharpayDatabaseFactory : IDatabaseFactory<EgharpayDatabase>
    {
        public string NameOrConnectionString { get; }

        public EgharpayDatabaseFactory(string nameOrConnectionString)
        {
            NameOrConnectionString = nameOrConnectionString;
        }

        public EgharpayDatabase CreateContext()
        {
            ValidateConnectionString();
            var context = new EgharpayDatabase(NameOrConnectionString);
           // context.UseSerilog();

            return context;
        }

        private void ValidateConnectionString()
        {
            if (string.IsNullOrWhiteSpace(NameOrConnectionString))
                throw new NullReferenceException("OmbrosDatabaseFactory expects a valid NameOrConnectionString");
        }
    }
}
