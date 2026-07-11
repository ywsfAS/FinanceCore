namespace FinanceCore.API.Models
{
    public static class ErrorCodes
    {
        public const string Blanc = "blanc";
        // Validation Errors (400)
        public const string ValidationError = "VALIDATION_ERROR";
        public const string InvalidInput = "INVALID_INPUT";
        public const string MissingField = "MISSING_FIELD";

        // Authentication Errors (401)
        public const string Unauthorized = "UNAUTHORIZED";
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string TokenExpired = "TOKEN_EXPIRED";
        public const string InvalidToken = "INVALID_TOKEN";

        // Authorization Errors (403)
        public const string Forbidden = "FORBIDDEN";
        public const string InsufficientPermissions = "INSUFFICIENT_PERMISSIONS";
        public const string AccessDenied = "ACCESS_DENIED";

        // Not Found Errors (404)
        public const string ResourceNotFound = "RESOURCE_NOT_FOUND";
        public const string EntityNotFound = "ENTITY_NOT_FOUND";

        // Conflict Errors (409)
        public const string Conflict = "CONFLICT";
        public const string DuplicateResource = "DUPLICATE_RESOURCE";
        public const string ResourceAlreadyExists = "RESOURCE_ALREADY_EXISTS";

        // Server Errors (500)
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
        public const string UnhandledException = "UNHANDLED_EXCEPTION";
        public const string DatabaseError = "DATABASE_ERROR";
        // Domain Errors (400)
        public const string DomainError = "DOMAIN_ERROR";
        public const string BusinessLogicError = "BUSINESS_LOGIC_ERROR";
    }
}
