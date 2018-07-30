using System;
using DataLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EntityLayer;
using DataAccessOperationEntity;

namespace BussinessLayer
{
    public class BussinessLogicOperation
    {
        DataAccessOperation DataAccessObject = new DataAccessOperation();
        DataAccessLayerEntity EntityDataAccessObject = new DataAccessLayerEntity();
        public int AddingNewAccount(BankEntities EntityObject,int KeyValue)
        {
            int a;
            if (KeyValue == 1)
                a = DataAccessObject.AddingAccount("AddingAccountDetails", EntityObject);
            else
             a = EntityDataAccessObject.AddingAccount(EntityObject); 
            return a;
        }
        public BankEntities[] DisplayAllAccountsDetails(int KeyValue)
        {
            BankEntities[] EntityArray;
            if (KeyValue==1)
            EntityArray= DataAccessObject.DisplayAllDetails();
            else
             EntityArray= EntityDataAccessObject.DisplayAllDetails();
            return EntityArray;
        }
        public BankEntities DisplayAccountofParticularAccount(int AccNum,int KeyValue)
        {
            BankEntities EntityObject = new BankEntities();
            if(KeyValue==1)
            EntityObject = DataAccessObject.getAccountDetail(AccNum);
            else
            EntityObject=EntityDataAccessObject.getAccountDetails(AccNum);
            return EntityObject;
        }
        public int Withdrawing(int AccNum,int Amount,int KeyValue)
        {
            int Balance;
            if (KeyValue == 1)
                Balance = DataAccessObject.WithdrawingAmount(AccNum, Amount);
            else
                Balance = EntityDataAccessObject.WithdrawingAmount(AccNum, Amount);
            return Balance;
        }
        public int Depositing(int AccNum,int Amount,int KeyValue)
        {
            int Balance;
            if(KeyValue==1)
            Balance = DataAccessObject.DepositingAmount(AccNum, Amount);
            else
            Balance = EntityDataAccessObject.DepositingAmount(AccNum, Amount);
            return Balance;
        }
        public double CalculatingInterest(int AccNum,int KeyValue)
        {
            double Interest;
            if (KeyValue == 1)
                Interest = DataAccessObject.AccountInterest(AccNum);
            else
                Interest = EntityDataAccessObject.AccountInterest(AccNum);
            return Interest;
        }

    }
}
