namespace ZeroStoreApp.CrossCutting.Constants;

public static class Definitions
{
    public const string IdRequiredMessage = "Id is required";
    public static class PaginationDefinition
    {
        public const int DefaultPage = 1;
        public const int MaxPageSize = 100;
        public const int DefaultPageSize = 10;

        public const string PageRangeMessage = "Page must be greater than 1";

        public const string PageSizeRangeMessage = "Page size must be between 1 and {MaxPageSize}";
    }
    public static class ProductDefinition
    {
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 500;
        public const int PricePrecision = 18;
        public const int PriceScale = 2;

        public const string NameRequiredMessage = "Name is required";
        public const string NameMaxLengthMessage = "Name must be less than {MaxLength} characters";

        public const string DescriptionRequiredMessage = "Description is required";
        public const string DescriptionMaxLengthMessage = "Description must be less than {MaxLength} characters";

        public const string PriceRequiredMessage = "Price is required";
        public const string PricePrecisionScaleMessage = "Price must have {Precision} digits and {Scale} decimals";
        public const string PricePositive = "Price must be positive";

        public const string StockRequiredMessage = "Stock is required";
        public static string StockPositive = "Stock must be positive";
    }
}
