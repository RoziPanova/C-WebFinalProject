namespace AspNetCoreArchTemplate.Data.Common
{
    public static class EntityConstants
    {
        public static class Bouquet
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;
        }
        public static class Arrangement
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 500;

        }
        public static class CustomOrder
        {
            public const int CustomOrderDetailsMinLenght = 100;
            public const int CustomOrderDetailsMaxLenght = 1000;
        }
        public static class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;
        }
    }
}
