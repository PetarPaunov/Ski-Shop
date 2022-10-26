namespace SkiShop.Data.Common
{
    public class DataConstants
    {
        public class Product
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 20;

            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 1000;

            public const string PriceMinValue = "50";
            public const string PriceMaxValue = "2000";

            public const double NoseWidthMinValue = 20;
            public const double NoseWidthMaxValue = 140;

            public const double TailWidthMinValue = 20;
            public const double TailWidthMaxValue = 140;

            public const double WaistWidthMinValue = 20;
            public const double WaistWidthMaxValue = 140;
        }

        public class Comment
        {
            public const int TitleMaxLength = 20;
            public const int TitleMinLength = 5;

            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 10;
        }
    }
}
