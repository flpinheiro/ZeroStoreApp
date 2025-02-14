namespace ZeroStoreApp.CrossCutting.Constants;

public static class ResponseMessages
{
    public static class Products 
    {
        public const string ProductNotFound = "Product {0} not found";
        public const string ProductCreated = "Product {0} created";
        public const string ProductUpdated = "Product {0} updated";
        public const string ProductDeleted = "Product {0} deleted";

        public const string ProductNotCreated = "Product {0} not created";
        public const string ProductNotUpdated = "Product {0} not updated";
        public const string ProductNotDeleted = "Product {0} not deleted";
    }
    

    public const string ValidationFailed = "Validation failed";
}