using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobaSpace.Core.Data.Migrations
{
    public static class MigrationExtensions
    {
        public static void GrantOnTable(this MigrationBuilder migration, in string shem, in string table, in string user, params string[] rights)
        {
            migration.Sql($"GRANT {string.Join(", ", rights)} ON TABLE \"{ shem}\".\"{table}\" TO {user}");
        }
    }
}
