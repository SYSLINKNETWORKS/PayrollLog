namespace TWP_API_Log.Generic
{
    public class Enums
    {
        public enum ClassName
        {

            ErrorLog,
            ServiceTable,
            AuditLog,

        }
        public enum ErrorType
        {
            Error,
            Information,
            Warning,

        }
        public enum Misc
        {
            UserId,
            UserName,

            CompanyId,
            CompanyName,
            BranchId,
            BranchName,
            Key,
        }

        public enum Operations
        {
            A,
            E,
            D,
            G, //Get All Data
            I, //Get by ID

        }

    }
}