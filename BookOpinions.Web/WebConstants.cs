namespace BookOpinions.Web
{
    public class WebConstants
    {
        public const string AdminRole = "Admin";
        public const string BooksModeratorRole = "BooksModerator";
        public const string ShopModeratorRole = "ShopModerator";
        public const string CommentsModeratorRole = "CommentsModerator";
        public const string AddingBooksRole = "AddingBooks";

        public static readonly string AddBookRoles = string.Join(", ",
            AdminRole, BooksModeratorRole, ShopModeratorRole, CommentsModeratorRole, AddingBooksRole);

        public const int PasswordMinLength = 4;

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";
        public const string TempDataAddedBookMessageKey = "TempDataAddedBookMessage";
        public const string TempDataDeletedBookMessageKey = "TempDataDeletedBookMessage";

        public const string AdminArea = "Admin";

        public const int PopularBooksCount = 12;

        public const int BooksAllBooksOnPage = 6 * 3;
    }
}
