using SchoolManagement.Debugging;

namespace SchoolManagement
{
    public class SchoolManagementConsts
    {
        public const string LocalizationSourceName = "SchoolManagement";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "f9562b674e9444c6ba99f697162ad059";
    }
}
