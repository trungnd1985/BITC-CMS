namespace BITC.CMS.Identity.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.Linq;

    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> where TUser: IdentityUser
    {
        public IdentityDbContext() : this("DefaultConnection")
        {
        }

        public IdentityDbContext(string nameOrConnectionString) : this(nameOrConnectionString, true)
        {
        }

        public IdentityDbContext(string nameOrConnectionString, bool throwIfV1Schema) : base(nameOrConnectionString)
        {
            if (throwIfV1Schema && IdentityDbContext<TUser>.IsIdentityV1Schema(this))
            {
                throw new InvalidOperationException(IdentityResources.IdentityV1SchemaError);
            }
        }

        public IdentityDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {
        }

        internal static bool IsIdentityV1Schema(DbContext db)
        {
            SqlConnection connection = db.Database.Connection as SqlConnection;
            if ((connection != null) && db.Database.Exists())
            {
                using (SqlConnection connection2 = new SqlConnection(connection.ConnectionString))
                {
                    connection2.Open();
                    return (((IdentityDbContext<TUser>.VerifyColumns(connection2, "AspNetUsers", new string[] { "Id", "UserName", "PasswordHash", "SecurityStamp", "Discriminator" }) && IdentityDbContext<TUser>.VerifyColumns(connection2, "AspNetRoles", new string[] { "Id", "Name" })) && (IdentityDbContext<TUser>.VerifyColumns(connection2, "AspNetUserRoles", new string[] { "UserId", "RoleId" }) && IdentityDbContext<TUser>.VerifyColumns(connection2, "AspNetUserClaims", new string[] { "Id", "ClaimType", "ClaimValue", "User_Id" }))) && IdentityDbContext<TUser>.VerifyColumns(connection2, "AspNetUserLogins", new string[] { "UserId", "ProviderKey", "LoginProvider" }));
                }
            }
            return false;
        }

        internal static bool VerifyColumns(SqlConnection conn, string table, params string[] columns)
        {
            List<string> list = new List<string>();
            using (SqlCommand command = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@Table", conn))
            {
                command.Parameters.Add(new SqlParameter("Table", table));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
            }
            return columns.All<string>(new Func<string, bool>(list.Contains));
        }
    }
}

