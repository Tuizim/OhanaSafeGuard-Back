namespace Ohana.MessageLibrary
{
    public static class ErrorMessages
    {
        #region Servidor
        
        public const string InvalidCredentials = "The provided credentials are invalid.";
        public const string AccessDenied = "You do not have permission to perform this action.";
        public const string RequiredFieldMissing = "A required field is missing.";
        public const string ServerError = "An unexpected error occurred on the server.";

        #endregion
        #region Busca
        public const string NotFound = "The requested item was not found.";
        public const string JustExist = "There is already a similar field created for this user";
        public const string CantDelete = "It will not be possible to delete this item as it has not been located.";
        #endregion
        #region Insert
        public const string PostError = "An unexpected error occurred when we tried to insert.";
        public const string UpdateError = "An unexpected error occurred when we tried to update.";
        #endregion
    }
}
