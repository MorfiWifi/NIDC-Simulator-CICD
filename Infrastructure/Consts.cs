using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Consts
    {
        public const string Developer = "Developer";
        public const string AdminRole = "Admin";
        public const string ModeratorRole = "Moderator";
        public const string CoordinatorRole = "Coordinator";
        public const string AssessorRole = "Assessor";
        public const string AdminOrDeveloper = "Admin,Developer";

        #region DrillingRoles
        public const string Driller = "Driller";
        public const string Assessor = "Assessor";

        #endregion

        public const bool SignUpEnabled = true;
        public const bool SendOtpSms = true;

        public const string CompanyLogosFolder = "CompanyLogos";

        // public static string BaseAddress = "https://api.wsim-app.ir/";
        //public static string BaseAddress = "https://localhost:44331/";
        // public static string BaseAddress = "http://localhost:5000/";
        // public static string BaseAddress = "http://0.0.0.0:5000/";
        // public static string BaseAddress = "http://192.168.73.133:5000/";
        // public static string BaseAddress = "https://sample-api.iran.liara.run/";
        // public static string BaseAddress = "http://78.109.201.86/"; // afranet
        public static string BaseAddress = "https://nidc.wsim-app.ir/"; // afranet

        public static string RedisAddress = "78.109.201.86:6379"; // afranet 
        // public static string RedisAddress = "85.198.9.229:6379";
        public static string RedisPassword = "1qazxsw2$$";

        //public static string RedisAddress = "aberama.iran.liara.ir:32815";
        //public static string RedisPassword = "4YKFnubfFFjfh4yTK7b0Rg9X";

        public static int SimulationStep = 500; //
        // public static int SimulationStep = 1000; //
        // public static int SimulationStep = 2000; //
    }
}
