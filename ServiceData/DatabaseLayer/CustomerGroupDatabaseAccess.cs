using ServiceData.DatabaseLayer.Interfaces;
using ServiceData.ModelLayer;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ServiceData.DatabaseLayer
{

    public class CustomerGroupDatabaseAccess : ICustomerGroup
    {

        readonly string? _connectionString;

        public CustomerGroupDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyConnection");
        }

        public CustomerGroupDatabaseAccess(string inConnectionString)
        {
            _connectionString = inConnectionString;
        }

        public int CreateCustomerGroup(CustomerGroup anCustomerGroup)
        {
            int insertedId = -1;
            //
            string insertString = "insert into CustomerGroup(name) OUTPUT INSERTED.ID values(@Name)";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand CreateCommand = new SqlCommand(insertString, con))
            {
                SqlParameter aCusGrpName = new("@Name", anCustomerGroup.Name);
                CreateCommand.Parameters.Add(aCusGrpName);
                con.Open();
                // Execute save and read generated key (ID)
                insertedId = (int)CreateCommand.ExecuteScalar();
            }
            return insertedId;
        }

        public bool DeleteCustomerGroupById(int id)
        {
            bool isDeleted = false;
            //
            string deleteString = "DELETE FROM CustomerGroup WHERE Id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand deleteCommand = new SqlCommand(deleteString, con))
            {
                deleteCommand.Parameters.AddWithValue("@Id", id);

                con.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                isDeleted = (rowsAffected > 0);
            }

            return isDeleted;
        }

        public List<CustomerGroup> GetAllCustomerGroup()
        {
            List<CustomerGroup> foundCusGrps;
            CustomerGroup readCusGrp;
            //
            string queryString = "SELECT Id, Name FROM CustomerGroup";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader CusGrpReader = readCommand.ExecuteReader();
                // Collect data
                foundCusGrps = new List<CustomerGroup>();
                while (CusGrpReader.Read())
                {
                    readCusGrp = GetCustomerGroupFromReader(CusGrpReader);
                    foundCusGrps.Add(readCusGrp);
                }
            }
            return foundCusGrps;
        }

        public CustomerGroup GetCustomerGroupById(int id)
        {
            CustomerGroup foundCusGrp;
            //
            string queryString = "select Id, name from CustomerGroup where id = @Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                //Prepare SQL
                SqlParameter idParam = new SqlParameter("@Id", id);
                readCommand.Parameters.Add(idParam);
                //
                con.Open();
                //Execute reead
                SqlDataReader cusGrpReader = readCommand.ExecuteReader();
                foundCusGrp = new CustomerGroup();
                while (cusGrpReader.Read())
                {
                    foundCusGrp = GetCustomerGroupFromReader(cusGrpReader);
                }
            }
            return foundCusGrp;
        }

        public bool UpdateCustomerGroupById(CustomerGroup customerGroupToUpdate)
        {
            bool isUpdated = false;
            string updateString = "UPDATE CustomerGroup SET name = @Name WHERE Id = @Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand updateCommand = new SqlCommand(updateString, con))
            {
                updateCommand.Parameters.AddWithValue("@Id", customerGroupToUpdate.Id);
                updateCommand.Parameters.AddWithValue("@Name", customerGroupToUpdate.Name);

                con.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (isUpdated = (rowsAffected > 0))
                {
                    return isUpdated;
                }
                else
                {
                    return false;
                }
            }
        }
        private CustomerGroup GetCustomerGroupFromReader(SqlDataReader cusGrpReader)
        {
            CustomerGroup foundCusGrp;
            string tempName;
            int tempID;
            //fetch values
            tempID = cusGrpReader.GetInt32(cusGrpReader.GetOrdinal("Id"));
            tempName = cusGrpReader.GetString(cusGrpReader.GetOrdinal("Name"));
            //Create ingredient object
            foundCusGrp = new CustomerGroup(tempID, tempName);


            return foundCusGrp;
        }
    }
}
