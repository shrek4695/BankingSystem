using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace DataAccessOperationEntity
{
    public class DataAccessLayerEntity
    {
        public int AddingAccount(BankEntities FieldsObject)
        {
            try
            {
                BankDatabaseEntities bankDatabaseEntities = new BankDatabaseEntities();
                AccountDetail accountDetail = new AccountDetail();
                accountDetail.AccountNumber = FieldsObject.AccountNumber;
                accountDetail.Balance = FieldsObject.AccountBalance;
                accountDetail.BranchName = FieldsObject.BranchName;
                accountDetail.AccountType = FieldsObject.AccountType;
                bankDatabaseEntities.AccountDetails.Add(accountDetail);
                bankDatabaseEntities.SaveChanges();
                return 1;
            }
            catch(Exception e)
            {
                //Console.WriteLine(e.Message);
                return -10000;
            }
        }
        public BankEntities[] DisplayAllDetails()
        {
            BankDatabaseEntities bankDatabaseEntities = new BankDatabaseEntities();
            List<AccountDetail> list = new List<AccountDetail>();
            list = bankDatabaseEntities.AccountDetails.ToList();
            BankEntities[] EntityArray= new BankEntities[100];
            int counter = 0;
            foreach (AccountDetail accountDetailObject in list)
            {
                BankEntities bankEntities = new BankEntities();
                bankEntities.AccountNumber = accountDetailObject.AccountNumber;
                bankEntities.AccountBalance = accountDetailObject.Balance;
                bankEntities.BranchName = accountDetailObject.BranchName;
                bankEntities.AccountType = accountDetailObject.AccountType;
                EntityArray[counter] = bankEntities;
                counter++;
            }
            return EntityArray;
        }
        public BankEntities getAccountDetails(int AccNum)
        {
            try
            {
                BankDatabaseEntities bankDatabaseEntities = new BankDatabaseEntities();
                BankEntities EntityObject = new BankEntities();
                EntityObject.AccountNumber = bankDatabaseEntities.AccountDetails.Find(AccNum).AccountNumber;
                EntityObject.AccountBalance = bankDatabaseEntities.AccountDetails.Find(AccNum).Balance;
                EntityObject.BranchName = bankDatabaseEntities.AccountDetails.Find(AccNum).BranchName;
                EntityObject.AccountType = bankDatabaseEntities.AccountDetails.Find(AccNum).AccountType;
                return EntityObject;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public int WithdrawingAmount(int AccNum, int Amount)
        {
            try
            {
                BankDatabaseEntities bankDatabaseEntities = new BankDatabaseEntities();
                int bal = bankDatabaseEntities.AccountDetails.Find(AccNum).Balance;
                String AccType = bankDatabaseEntities.AccountDetails.Find(AccNum).AccountType;
                AccType = AccType.Trim();
                if (AccType == "Current")
                {
                    if (bal - Amount >= 0)
                        bal = bal - Amount;
                    else
                        return -10000;
                }
                else if (AccType == "Savings")
                {
                    if (bal - Amount >= 1000)
                        bal = bal - Amount;
                    else
                        return -10000;
                }
                else
                {
                    if (bal - Amount >= -1000)
                        bal = bal - Amount;
                    else
                        return -10000;
                }
                bankDatabaseEntities.AccountDetails.Find(AccNum).Balance = bal;
                bankDatabaseEntities.SaveChanges();
                return bal;
            }
            catch(Exception e)
            {
                return -10001;
            }
        }
        public int DepositingAmount(int AccNum,int Amount)
        {
            try
            {
                BankDatabaseEntities bankDatabaseEntities = new BankDatabaseEntities();
                int bal = bankDatabaseEntities.AccountDetails.Find(AccNum).Balance;
                bal = bal + Amount;
                bankDatabaseEntities.AccountDetails.Find(AccNum).Balance = bal;
                bankDatabaseEntities.SaveChanges();
                return bal;
            }
            catch(Exception e)
            {
                return -10001;
            }
        }
        public double AccountInterest(int AccNum)
        {
            try
            {
                BankDatabaseEntities bankDatabaseEntities = new BankDatabaseEntities();
                int bal = bankDatabaseEntities.AccountDetails.Find(AccNum).Balance;
                string AccType = bankDatabaseEntities.AccountDetails.Find(AccNum).AccountType;
                AccType = AccType.Trim();
                double interest;
                if (AccType == "Current")
                {
                    interest = (double)bal * 1.0 / 100.0;
                }
                else if (AccType == "Savings")
                {
                    interest = (double)bal * 4.0 / 100.0;
                }
                else
                {
                    interest = 0;
                }
                return interest;
            }
            catch(Exception e)
            {
                return -10001;
            }
        }

    }
}
