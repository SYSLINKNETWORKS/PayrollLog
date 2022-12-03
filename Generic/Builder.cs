using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TWP_API_Log.Bussiness;
using TWP_API_Log.Generic;
using TWP_API_Log.Models;

namespace TWP_API_Log.Generic
{
    public static class Builder
    {
        public static AbsBusiness MakeBusinessClass(Enums.ClassName ClassName, App_Data.DataContext _context)
        {
            switch (ClassName)
            {

                case Enums.ClassName.ErrorLog:
                    {
                        return new BLog(_context);
                    }
                case Enums.ClassName.ServiceTable:
                    {
                        return new BService(_context);
                    }
                case Enums.ClassName.AuditLog:
                    {
                        return new BAudit(_context);
                    }

                default:
                    return null;
            }
        }

    }
}