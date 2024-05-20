using System.Diagnostics;
using System;

namespace DataLayer.Database
{
    partial class DatabaseDataContext
    {
        /// <summary>
        /// Helper method to quickly remove all entities from the table.
        /// This method is intended to be used in unit tests so it is marked by the <see cref="Conditional"/> attribute and implemented for the "DEBUG" configuration only.
        /// </summary>
        [Conditional("DEBUG")]
        public void TruncateAllData()
        {
            Users.DeleteAllOnSubmit<User>(Users);
            Orders.DeleteAllOnSubmit<Order>(Orders);
            SubmitChanges();
        }
    }
}