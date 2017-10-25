namespace Egharpay.Models.Authorization
{
    public static class Policies
    {

        public enum Permission
        {
            SuperUser,
            Administrator_Admin,
            Personnel
        }

        public enum Resource
        {
            Personnel
        }
    }
}