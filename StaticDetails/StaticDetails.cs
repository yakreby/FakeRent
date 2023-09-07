namespace FakeRent.Utility
{
    public static class StaticDetails
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public static string SessionToken = "JWTToken";

        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";
    }
}