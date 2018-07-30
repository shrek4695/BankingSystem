using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EntityLayer;


namespace DataLayer
{
    public class DataAccessOperation
    {
        public int AddingAccount(String ProcedureName,BankEntities EntityObject)
        {
           try
            { 
                SqlConnection ConnectionObject = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
                ConnectionObject.Open();
                SqlCommand cmd = new SqlCommand(ProcedureName, ConnectionObject);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccountNumber", EntityObject.AccountNumber);
                cmd.Parameters.AddWithValue("@Balance", EntityObject.AccountBalance);
                cmd.Parameters.AddWithValue("@BranchName", EntityObject.BranchName);
                cmd.Parameters.AddWithValue("@AccountType", EntityObject.AccountType);
                int a = cmd.ExecuteNonQuery();
                ConnectionObject.Close();
                return a;
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
                return -1;
            }
        }
        public BankEntities[] DisplayAllDetails()
        {
            
            SqlConnection ConnectionObject = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
            BankEntities[] EntityArray = new BankEntities[100];
            string command = "select * from AccountDetails;";
            SqlCommand cmd = new SqlCommand(command,ConnectionObject);
            ConnectionObject.Open();
            SqlDataReader SqlReader = cmd.ExecuteReader();
            int counter = 0;
                while (SqlReader.Read())
                {
                    BankEntities EntityObject = new BankEntities();
                    EntityObject.AccountNumber = Convert.ToInt32(SqlReader.GetValue(0));
                    EntityObject.AccountBalance = Convert.ToInt32(SqlReader.GetValue(1));
                    EntityObject.BranchName = SqlReader.GetValue(2).ToString();
                    EntityObject.AccountType = SqlReader.GetValue(3).ToString();
                    EntityArray[counter] = EntityObject;
                    counter++;
                }
               
            ConnectionObject.Close();
            return EntityArray;
        }
        public BankEntities getAccountDetail(int AccNum)
        {
            try
            {
                BankEntities EntityObject = new BankEntities();
                SqlConnection ConnectionObject = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
                BankEntities[] EntityArray = new BankEntities[100];
                string command = "select * from AccountDetails;";
                SqlCommand cmd = new SqlCommand(command, ConnectionObject);
                ConnectionObject.Open();
                SqlDataReader SqlReader = cmd.ExecuteReader();
                while (SqlReader.Read())
                {
                    if (Convert.ToInt32(SqlReader.GetValue(0)) == AccNum)
                    {
                        EntityObject.AccountNumber = Convert.ToInt32(SqlReader.GetValue(0));
                        EntityObject.AccountBalance = Convert.ToInt32(SqlReader.GetValue(1));
                        EntityObject.BranchName = SqlReader.GetValue(2).ToString();
                        EntityObject.AccountType = SqlReader.GetValue(3).ToString();
                        break;
                    }
                }
                ConnectionObject.Close();
                return EntityObject;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public int WithdrawingAmount(int AccNum,int Amount)
        {
            try
            {
                int Balance = 0;
                string AccType = "";
                SqlConnection ConnectionObject = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
                string command = "select * from dbo.AccountDetails";

                SqlCommand cmd = new SqlCommand(command, ConnectionObject);
                ConnectionObject.Open();
                cmd.CommandText = command;
                SqlDataReader SqlReader = cmd.ExecuteReader();
                
                    while (SqlReader.Read())
                    {
                        if (Convert.ToInt32(SqlReader.GetValue(0)) == AccNum)
                        {
                            Balance = Convert.ToInt32(SqlReader.GetValue(1));
                            AccType = SqlReader.GetValue(3).ToString();
                            //_NextResult = false;
                            break;
                        }
                    }
                SqlReader.Close();
                AccType=AccType.Trim();
                //Console.WriteLine(AccType);
                //SqlConnection ConObj = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
                if (AccType == "Current")
                {
                    if (Balance - Amount >= 0)
                        Balance = Balance - Amount;
                    else
                        return -10000;
                }
                else if (AccType == "Savings")
                {
                    if (Balance - Amount >= 1000)
                        Balance = Balance - Amount;
                    else
                        return -10000;
                }
                else
                {
                    if (Balance - Amount > -1000)
                        Balance = Balance - Amount;
                    else
                        return -10000;
                }

                command = "update AccountDetails set Balance='" + Balance + "' where AccountNumber='" + AccNum + "'";
                SqlCommand com = new SqlCommand(command, ConnectionObject);
                com.ExecuteNonQuery();
                ConnectionObject.Close();
                return Balance;
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
                return -10001;
            }

        }
        public int DepositingAmount(int AccNum, int Amount)
        {
            try
            {
                int Balance = 0;
                SqlConnection ConnectionObject = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
                BankEntities[] EntityArray = new BankEntities[100];
                string command = "select * from dbo.AccountDetails";
                SqlCommand cmd = new SqlCommand(command, ConnectionObject);
                ConnectionObject.Open();
                SqlDataReader SqlReader = cmd.ExecuteReader();
                //bool _NextResult = true;
                //while (_NextResult == true)
                //{
                    while (SqlReader.Read())
                    {
                        if (Convert.ToInt32(SqlReader.GetValue(0)) == AccNum)
                        {
                            Balance = Convert.ToInt32(SqlReader.GetValue(1));
                            //_NextResult = false;
                            break;
                        }
                    }
                // _NextResult = SqlReader.NextResult();
                //if (_NextResult == false)
                //  break;
                //}
                SqlReader.Close();
                Balance = Balance + Amount;
                command = "update AccountDetails set Balance='" + Balance + "' where AccountNumber='" + AccNum + "'";
                cmd = new SqlCommand(command, ConnectionObject);
                cmd.ExecuteNonQuery();
                ConnectionObject.Close();
                return Balance;
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
                return -10001;
            }

        }
        public double AccountInterest(int AccNum)
        {
            try
            {
                double Interest;
                int Balance = 0;
                string AccType="";
                SqlConnection ConnectionObject = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
                string command = "select * from dbo.AccountDetails";

                SqlCommand cmd = new SqlCommand(command, ConnectionObject);
                ConnectionObject.Open();
                cmd.CommandText = command;
                SqlDataReader SqlReader = cmd.ExecuteReader();

                while (SqlReader.Read())
                {
                    if (Convert.ToInt32(SqlReader.GetValue(0)) == AccNum)
                    {
                        Balance = Convert.ToInt32(SqlReader.GetValue(1));
                        AccType = SqlReader.GetValue(3).ToString();
                        //_NextResult = false;
                        break;
                    }
                }
                SqlReader.Close();
                AccType=AccType.Trim();
                //Console.WriteLine(AccType);
                //SqlConnection ConObj = new SqlConnection("Data Source=TAVDESK001;initial catalog=BankDatabase;integrated security=true");
                if (AccType == "Current")
                {
                    Interest = 1 * (double)Balance / 100;
                }
                else if (AccType == "Savings")
                {
                    Interest = 4 * (double)Balance / 100;
                }
                else
                {
                    Interest = 0;
                }
                ConnectionObject.Close();
                Console.WriteLine(Interest);
                return Interest;

            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
                return -10001;
            }
        }
    }
}
