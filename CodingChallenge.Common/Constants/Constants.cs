using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Common.Constants
{
    public static class Constants
    {
        #region Errors

        public static class Error
        {
            /// <summary>
            /// Error constant for database concurrency issues.
            /// </summary>
            public const string ERROR_CONCURRENCY = "DbConcurrencyError";

            /// <summary>
            /// Error constant for general server errors.
            /// </summary>
            public const string ERROR_Server = "A server error has occurred.";
        }

        #endregion Errors

        #region Database Schema

        public static class DbSchema
        {
            /// <summary>
            /// The name of the database schema.
            /// </summary>
            public const string Name = "coding_challenge";
        }

        #endregion Database Schema
    }
}
