using ArtemisComm;
using ArtemisComm.Proxy.Logger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.Proxy.UI.SQLLogger
{
    public class PacketToLogging : IPacketToLogging, IDisposable
    {
        public PacketToLogging(string connectionstring)
        {
            ConnectionString = connectionstring;
        }
        public string ConnectionString { get; private set; }
        object lockingObject = new object();
        public object Process(Packet packet, Guid sourceID, Guid targetID, int subPacketType)
        {
            int seqID = 0;
            lock (lockingObject)
            {
                if (ActiveConnection == null)
                {
                    ActiveConnection = GetConnection();
                }
                seqID = InsertIntoPacketTable(packet, sourceID, targetID, subPacketType);
               
            }
            return seqID;
        }
        object lockingObject2 = new object();
        int InsertIntoPacketTable(Packet p, Guid sourceID, Guid targetID, int subPacketType)
        {
            int retVal = -1;
            lock (lockingObject2)
            {
                using (SqlCommand cmd =
                    new SqlCommand("INSERT INTO Packets (SourceID, TargetID, Length, Origin, Unknown, PayloadLength, PacketType, SubPacketType, Payload) "
                    + "VALUES (@SourceID, @TargetID, @Length, @Origin, @Unknown, @PayloadLength, @PacketType, @SubPacketType, @Payload); SELECT SCOPE_IDENTITY();",
                    ActiveConnection))
                {

                    int packType = (int)p.PacketType;
                    cmd.Parameters.AddWithValue("@SourceID", sourceID.ToString());
                    cmd.Parameters.AddWithValue("@TargetID", targetID.ToString());
                    cmd.Parameters.AddWithValue("@Length", p.Length);
                    cmd.Parameters.AddWithValue("@Origin", (int)p.Origin);
                    cmd.Parameters.AddWithValue("@Unknown", p.Padding);
                    cmd.Parameters.AddWithValue("@PayloadLength", p.PayloadLength);
                    cmd.Parameters.AddWithValue("@PacketType", packType);


                    cmd.Parameters.AddWithValue("@SubPacketType", subPacketType);


                    cmd.Parameters.AddWithValue("@Payload", p.Payload.GetBuffer());
                    decimal insertedID = (decimal)cmd.ExecuteScalar();
                    retVal = Convert.ToInt32(insertedID);
                }
            }
            return retVal;
        }

        public void AddException(Guid sourceID, Exception error)
        {
            if (error != null)
            {
                lock (lockingObject2)
                {
                    using (SqlCommand cmd =
                           new SqlCommand("INSERT INTO Exceptions (SourceID, Message, StackTrace, ExceptionType) "
                           + "VALUES (@SourceID, @Message, @StackTrace, @ExceptionType);",
                           ActiveConnection))
                    {
                        cmd.Parameters.AddWithValue("@SourceID", sourceID.ToString());
                        cmd.Parameters.AddWithValue("@Message", error.Message);
                        cmd.Parameters.AddWithValue("@StackTrace", error.StackTrace);
                        cmd.Parameters.AddWithValue("@ExceptionType", error.GetType().ToString());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        SqlConnection GetConnection()
        {
            SqlConnection retVal = null;
            SqlConnection sql = null;
            try
            {
                sql = new SqlConnection(ConnectionString);
                sql.Open();
                retVal = sql;
            }
            finally
            {
                if (retVal == null)
                {

                    sql.Dispose();
                }
            }
            return retVal;


        }
        SqlConnection ActiveConnection { get; set; }

        bool isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!isDisposed)
                {
                    if (ActiveConnection != null)
                    {
                        try
                        {
                            ActiveConnection.Close();
                        }
                        catch { }
                    }
                    isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        public void ProcessValues(object key, string propertyName, object value, string propertyType, string hexValue)
        {
            lock (lockingObject2)
            {
                using (SqlCommand cmd =
                    new SqlCommand("INSERT INTO [Values] (PacketSequenceID, Property, PropertyType, Value, HexValue) "
                    + "VALUES (@PacketSequenceID, @Property, @PropertyType, @Value, @HexValue);",
                    ActiveConnection))
                {
                    cmd.Parameters.AddWithValue("@PacketSequenceID", (int)key);
                    cmd.Parameters.AddWithValue("@Property", propertyName);
                    cmd.Parameters.AddWithValue("@PropertyType", propertyType);
                    cmd.Parameters.AddWithValue("@Value", value);
                    cmd.Parameters.AddWithValue("@HexValue", hexValue);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
