using Microsoft.Extensions.Configuration;
using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ServiceData.DatabaseLayer
{
    public class OrdersDatabaseAccess : IOrders
    {
        readonly string? _connectionString;

        public OrdersDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public OrdersDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }
        //Creates a new order in the database and associates it with the specified shop.
        //param name="aOrder"
        //Returns The ID of the newly created order.
        public async Task<int> CreateOrder(Orders aOrder)
        {
            int insertedId = -1;
            string insertString = "INSERT INTO Orders(OrderNumber, DateTime, TotalPrice, ShopID) OUTPUT INSERTED.ID " +
                "VALUES (@OrderNumber, @DateTime, @TotalPrice, @ShopID)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand createCommand = new SqlCommand(insertString, con, transaction))
                        {
                            createCommand.Parameters.AddWithValue("@OrderNumber", aOrder.OrderNumber);
                            createCommand.Parameters.AddWithValue("@DateTime", DateTime.Now);
                            createCommand.Parameters.AddWithValue("@TotalPrice", aOrder.TotalPrice);
                            createCommand.Parameters.AddWithValue("@ShopID", aOrder.ShopId);

                            insertedId = (int)await createCommand.ExecuteScalarAsync();
                        }


                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        // Handle exceptions, log, or roll back the transaction
                        transaction.Rollback();
                        throw; // Rethrow the exception
                    }
                }
            }

            return insertedId;
        }

        public async Task<bool> DeleteOrderById(int id)
        {
            bool isDeleted = false;
            string deleteString = "DELETE FROM Orders WHERE Id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                int rowsAffected = await deleteCommand.ExecuteNonQueryAsync();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public async Task<List<Orders>> GetAllOrders()
        {
            List<Orders> foundOrders = new List<Orders>();
            string queryString = "SELECT * FROM Orders";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                await con.OpenAsync();
                SqlDataReader ordersReader = await readCommand.ExecuteReaderAsync();
                while (await ordersReader.ReadAsync())
                {
                    Orders readOrder = GetOrdersFromReader(ordersReader);
                    foundOrders.Add(readOrder);
                }
            }
            return foundOrders;
        }

        public async Task<Orders> GetOrderById(int id)
        {
            Orders foundOrder = new Orders();
            string queryString = "SELECT * FROM Orders WHERE Id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                readCommand.Parameters.AddWithValue("@Id", id);
                await con.OpenAsync();
                SqlDataReader orderReader = await readCommand.ExecuteReaderAsync();
                while (await orderReader.ReadAsync())
                {
                    foundOrder = GetOrdersFromReader(orderReader);
                }
            }
            return foundOrder;
        }

        public async Task<bool> UpdateOrderById(Orders orderToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE Orders SET OrderNumber = @OrderNumber, DateTime = @DateTime, " +
                "TotalPrice = @TotalPrice, ShopID = @ShopId WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", orderToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@OrderNumber", orderToUpdate.OrderNumber);
                updateCommand.Parameters.AddWithValue("@DateTime", orderToUpdate.DateTime);
                updateCommand.Parameters.AddWithValue("@TotalPrice", orderToUpdate.TotalPrice);
                updateCommand.Parameters.AddWithValue("@ShopId", orderToUpdate.ShopId);

                await con.OpenAsync();
                int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

                isUpdated = (rowsAffected > 0);
            }

            return isUpdated;
        }

        private Orders GetOrdersFromReader(SqlDataReader ordersReader)
        {
            int readerId = ordersReader.GetInt32(ordersReader.GetOrdinal("Id"));
            int readerOrderNumber = ordersReader.GetInt32(ordersReader.GetOrdinal("OrderNumber"));
            DateTime readerDateTime = ordersReader.GetDateTime(ordersReader.GetOrdinal("DateTime"));
            decimal readerTotalPrice = ordersReader.GetDecimal(ordersReader.GetOrdinal("TotalPrice"));
            int readerShopId = ordersReader.GetInt32(ordersReader.GetOrdinal("ShopID"));

            return new Orders(readerId, readerOrderNumber, readerDateTime, readerTotalPrice, readerShopId);
        }
    }
}
