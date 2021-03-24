using EcoClean.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EcoClean.Controllers.Api
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {

        RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public AdminController(RoleManager<IdentityRole> manager, ApplicationDbContext dbContext)
        {
            _roleManager = manager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> GetRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "ADMIN" });
            return View(await _roleManager.Roles.ToListAsync());
        }
        public IActionResult Index()
        {
            return View();
        }

        // database
        [HttpPost]
        [Route("backupDatabase")]
        public async Task<string> BackupDatabase()
        {
            // read connectionstring from config file
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-PetthyDb;Trusted_Connection=True;MultipleActiveResultSets=true";

            // read backup folder from config file ("C:/temp/")
            var backupFolder = "C:/Program Files/Microsoft SQL Server/MSSQL12.SQLEXPRESS/MSSQL/Backup/";

            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                backupFolder, sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return "Backup done";
                }
            }
        }


        [HttpPost]
        [Route("restoreDatabase")]
        private void RestoreDatabase(string localDatabasePath, string fileListDataName, string fileListLogName)
        {
            string localDownloadFilePath = "C:/Program Files/Microsoft SQL Server/MSSQL12.SQLEXPRESS/MSSQL/Backup/";
            Console.WriteLine(string.Format("Restoring database {0}...", localDatabasePath));
            string fileListDataPath = Directory.GetParent(localDownloadFilePath).Parent.FullName + @"\DATA\" + fileListDataName + ".mdf";
            string fileListLogPath = Directory.GetParent(localDownloadFilePath).Parent.FullName + @"\DATA\" + fileListLogName + ".ldf";

            string sql = @"RESTORE DATABASE @dbName FROM DISK = @path WITH RECOVERY,
        MOVE @fileListDataName TO @fileListDataPath,
        MOVE @fileListLogName TO @fileListLogPath";

            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-PetthyDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 7200;
                    command.Parameters.AddWithValue("@dbName", fileListDataName);
                    command.Parameters.AddWithValue("@path", localDatabasePath);
                    command.Parameters.AddWithValue("@fileListDataName", fileListDataName);
                    command.Parameters.AddWithValue("@fileListDataPath", fileListDataPath);
                    command.Parameters.AddWithValue("@fileListLogName", fileListLogName);
                    command.Parameters.AddWithValue("@fileListLogPath", fileListLogPath);

                    command.ExecuteNonQuery();
                }
            }
            Console.WriteLine(string.Format("Database restoration complete for {0}.", localDatabasePath));
        }
    }
}

