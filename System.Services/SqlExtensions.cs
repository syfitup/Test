using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Threading;

public static class SqlExtensions
{
    /*On very rare occurrences the database may not be available. If so, try 3 times, and then send out an email*/
    public static void OpenIfAvailable(this SqlConnection conn)
    {
        bool connectionAvailable = false;
        int connectionAttempts = 0;
        while (connectionAttempts < 4 && !connectionAvailable)
        {
            try
            {
                connectionAttempts++;
                conn.Open();
                connectionAvailable = true;
            }
            catch (Win32Exception)
            {
                if (connectionAttempts == 3)
                {
                    //Network connection is down. Send Email. Panic. "After network is back online, you need to restart the app pool or users will continue to get invalidtenant or datacontext errors"
                    throw;
                }
                Thread.Sleep(500);
            }
            catch (SqlException)
            {
                if (connectionAttempts == 3)
                {
                    //Database is down. Send Email.  "After database is back online, you need to restart the app pool or users will continue to get invalidtenant or datacontext errors"
                    throw;
                }
                Thread.Sleep(500);
            }
        }

    }
}
