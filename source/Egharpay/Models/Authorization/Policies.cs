namespace Egharpay.Models.Authorization
{
    public static class Policies
    {

        public enum Permission
        {
            SuperUser,
            Administrator_Admin,
            Administrator_Finance,
            Administrator_HR,
            Client_Admin,
            Client_Finance,
            Client_ManageWorker,
            Personnel
        }

        public enum Resource
        {
            Site,
            Client,
            ClientPersonnel,
            Worker,
            WorkerContract,
            Engager,
            Colleague,
            Document,
            WorkerPayment,
            Personnel
        }
    }
}