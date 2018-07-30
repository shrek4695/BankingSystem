using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EntityLayer;
using BussinessLayer;
using System.Configuration;
namespace BankingSystem
{
    public class Host
    {
        public enum AccType
        {
            Current= 1,
            Savings= 2,
            DMAT= 3
        };
        public static int KeyValue;
        BankEntities EntitiesObject = new BankEntities();
        BussinessLogicOperation BussinessObject = new BussinessLogicOperation();
        static void Main(string[] args)
        {
            int UserChoice;
            //string databaseCOnnectivity = ConfigurationManager.AppSettings["DatabaseConnectivity"].ToString();
            //Int32.TryParse(databaseCOnnectivity, out KeyValue);
            do
            {
                Console.Clear();
                Console.WriteLine("Banking System");
                Console.WriteLine("1-Bank Panel");
                Console.WriteLine("2-User Panel");
                Console.WriteLine("3-Exit");
                Console.WriteLine("Enter Your Choice");
                UserChoice = int.Parse(Console.ReadLine());
                if (UserChoice != 3)
                {
                    Console.WriteLine("Choose Database Access Type");
                    Console.WriteLine("1-ADO.NET");
                    Console.WriteLine("2-Entity Framework");
                    Console.WriteLine("Enter Your Choice");
                    KeyValue = int.Parse(Console.ReadLine());
                }
                switch(UserChoice)
                 {
                    case 1: BankAdmin();
                            break;
                    case 2: AccountUser();
                            break;
                    case 3: Console.WriteLine("Thank You");
                            break;
                    default:Console.WriteLine("Invalid Input");
                            break;
                 }
                Console.ReadKey();
            }while(UserChoice!=3);
            
        }
        public static void BankAdmin()
        {
            int UserChoice;
            Host HostObject = new Host();
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome Admin");
                Console.WriteLine("1-Add New Account Details");
                Console.WriteLine("2-Display Particular Account Details");
                Console.WriteLine("3-Display Details of All Accounts");
                Console.WriteLine("4-Logout");
                Console.WriteLine("Enter Your Choice");
                UserChoice = int.Parse(Console.ReadLine());
                switch(UserChoice)
                {
                    case 1: HostObject.AddingAccount();
                            break;
                    case 2: HostObject.SearchbyAccountNumber();
                            break;
                    case 3: HostObject.DisplayDetailsofAcccounts();
                            break;
                    case 4: Console.WriteLine("Thank You");
                            break;
                    default:Console.WriteLine("Invalid Input");
                            break;
                }
            } while (UserChoice!=4);
         }

        public static void AccountUser()
        {
            int UserChoice;
            Host HostObject = new Host();
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome User");
                Console.WriteLine("1-Display Account Details");
                Console.WriteLine("2-Withdraw Amount");
                Console.WriteLine("3-Deposit Amount");
                Console.WriteLine("4-Check Interest Amount");
                Console.WriteLine("5-Logout");
                Console.WriteLine("Enter Your Choice");
                UserChoice = int.Parse(Console.ReadLine());
                switch (UserChoice)
                {
                    case 1:
                        HostObject.SearchbyAccountNumber();
                        break;
                    case 2:
                        HostObject.WithdrawAmount();
                        break;
                    case 3:
                        HostObject.DepositAmount();
                        break;
                    case 4:
                        HostObject.CalculateInterest();
                        break;
                    case 5:
                        Console.WriteLine("Thank You");
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            } while (UserChoice != 5);
        }
        public void AddingAccount()
        {
            Console.WriteLine("Enter Account Number");
            EntitiesObject.AccountNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Account Balance");
            EntitiesObject.AccountBalance = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Branch Name");
            EntitiesObject.BranchName = Console.ReadLine();
            Console.WriteLine("Enter Account Type");
            Console.WriteLine("1-Current Type");
            Console.WriteLine("2-Savings Type");
            Console.WriteLine("3-DMAT");
            EntitiesObject.AccountType = Enum.GetName(typeof(AccType),int.Parse(Console.ReadLine()));
            int a=BussinessObject.AddingNewAccount(EntitiesObject,KeyValue);
            if(a==-1)
                Console.WriteLine("Error Occured");
            else
                Console.WriteLine(a+" Accounts Added");
            Console.ReadKey();
        }
        public void DisplayDetailsofAcccounts()
        {

            BankEntities[] EntityArray= BussinessObject.DisplayAllAccountsDetails(KeyValue);
            int count = 0;
            Console.WriteLine("Account Number\tBalance\t\tBranch Name\tAccountType");
            while(EntityArray[count]!=null)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}\t{3}",EntityArray[count].AccountNumber,EntityArray[count].AccountBalance,EntityArray[count].BranchName,EntityArray[count].AccountType);
                count++;
            }
            Console.ReadKey();
        }
        public void SearchbyAccountNumber()
        {
            BankEntities EntityObject = new BankEntities();
            Console.WriteLine("Enter Account Number");
            int AccNum = int.Parse(Console.ReadLine());
            EntityObject=BussinessObject.DisplayAccountofParticularAccount(AccNum,KeyValue);
            if(EntityObject==null)
            {
                Console.WriteLine("Invalid Account Number");
                return;
            }
            Console.WriteLine("Account Number="+EntityObject.AccountNumber);
            Console.WriteLine("Account Balance="+EntityObject.AccountBalance);
            Console.WriteLine("Branch Name="+EntityObject.BranchName);
            Console.WriteLine("Account Type="+EntityObject.AccountType);
            Console.ReadKey();
        }
        public void WithdrawAmount()
        {
            Console.WriteLine("Enter Account Number");
            int AccNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Withdraw Amount");
            int Amount = int.Parse(Console.ReadLine());
            int Balance=BussinessObject.Withdrawing(AccNum,Amount,KeyValue);
            if(Balance==-10000)
            {
                Console.WriteLine("Insufficient Balance");
            }
            else if(Balance==-10001)
            {
                Console.WriteLine("Invalid Account Number");
            }
            else
            {
                Console.WriteLine("Available Balance="+Balance);
            }
            Console.ReadKey();
      
        }
        public void DepositAmount()
        {
            Console.WriteLine("Enter Account Number");
            int AccNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Deposit Amount");
            int Amount = int.Parse(Console.ReadLine());
            int Balance = BussinessObject.Depositing(AccNum, Amount,KeyValue);
            if(Balance==-10001)
                Console.WriteLine("Invalid Account Number");
            else
            Console.WriteLine("Available Balance="+Balance);
            Console.ReadKey();
        }
        public void CalculateInterest()
        {
            Console.WriteLine("Enter Account Number");
            int AccNum = int.Parse(Console.ReadLine());
            double Interest = BussinessObject.CalculatingInterest(AccNum,KeyValue);
            if(Interest==-1001)
                Console.WriteLine("Invalid Account Number");
            else
            Console.WriteLine("Interest Calculated="+Interest);
            Console.ReadKey();
        }
    }
}
