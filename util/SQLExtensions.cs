using System;
using System.Data;

namespace Geico.Moat.App.Fnd.Data.ADO
{
    /// <summary>
    /// Helper functions for SQL interactions.
    /// pulled from https://geico.visualstudio.com/IBU.Multiline/_git/PKG.Fnd.Data.ADO?path=/src/Data.ADO/SqlExtensions.cs&_a=contents&version=GBmaster
    /// </summary>
    public static class SqlExtensions
    {
        /// <summary>
        /// Reads a <see cref="char"/> value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as a <see cref="char"/>.</returns>
        public static char? GetChar(this IDataReader reader, string colName)
        {
            try
            {
                if (reader[colName] == DBNull.Value)
                {
                    return null;
                }

                var strValue = reader[colName]?.ToString();

                return string.IsNullOrWhiteSpace(strValue) ? (char?)null : strValue[0];
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads a <see cref="Guid"/> value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as a <see cref="Guid"/>.</returns>
        public static Guid? GetGuid(this IDataReader reader, string colName)
        {
            try
            {
                if (reader[colName] == DBNull.Value)
                {
                    return null;
                }

                return reader[colName] as Guid?;
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads a string value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as a String.</returns>
        public static string GetString(this IDataReader reader, string colName)
        {
            try
            {
                if (reader[colName] == DBNull.Value)
                {
                    return null;
                }

                var strValue = reader[colName]?.ToString();

                return string.IsNullOrWhiteSpace(strValue) ? null : strValue;
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads an <see cref="int"/> value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as an <see cref="int"/>.</returns>
        public static int? GetInt32(this IDataReader reader, string colName)
        {
            try
            {
                return reader[colName] == DBNull.Value ? null : (int?)Convert.ToInt32(reader[colName]);
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads an <see cref="long"/> value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as an <see cref="long"/>.</returns>
        public static long? GetInt64(this IDataReader reader, string colName)
        {
            try
            {
                return reader[colName] == DBNull.Value ? null : (long?)Convert.ToInt64(reader[colName]);
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads an <see cref="decimal"/> value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as an <see cref="decimal"/>.</returns>
        public static decimal? GetDecimal(this IDataReader reader, string colName)
        {
            try
            {
                return reader[colName] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader[colName]);
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads an <see cref="short"/> value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as an <see cref="short"/>.</returns>
        public static short? GetShort(this IDataReader reader, string colName)
        {
            try
            {
                return reader[colName] == DBNull.Value ? null : (short?)Convert.ToInt16(reader[colName]);
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads a boolean value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as a bool.</returns>
        public static bool? GetBoolean(this IDataReader reader, string colName)
        {
            try
            {
                return reader[colName] == DBNull.Value ? null : (bool?)Convert.ToBoolean(reader[colName]);
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads a DateTime value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as a DateTime.</returns>
        public static DateTime? GetDateTime(this IDataReader reader, string colName)
        {
            try
            {
                return reader[colName] == DBNull.Value ? null : (DateTime?)DateTime.Parse(reader.GetString(colName));
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads a byte array (VARBINARY) value from the <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader">the data reader returned from SQL Server.</param>
        /// <param name="colName">The name of the column to read.</param>
        /// <returns>The data as a Byte array.</returns>
        public static byte[] GetBytes(this IDataReader reader, string colName)
        {
            try
            {
                return reader[colName] == DBNull.Value ? null : (byte[])reader[colName];
            }
            catch (IndexOutOfRangeException)
            {
                throw;
            }
        }
    }
}
