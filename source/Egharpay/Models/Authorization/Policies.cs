namespace Egharpay.Models.Authorization
{
    public static class Policies
    {

        public enum Permission
        {
            SuperUser,
            Admin,
            Seller,
            Personnel,
            AdministratorMobileRepair
        }

        public enum Resource
        {
            Personnel,
            Admin,
            Seller,
            MobileRepair
        }
    }
}