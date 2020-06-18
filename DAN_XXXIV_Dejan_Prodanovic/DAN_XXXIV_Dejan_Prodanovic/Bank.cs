using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XXXIV_Dejan_Prodanovic
{
    class Bank
    {

        private object thisLock = new object();
        public int TotalAmount { get; set; }
        
        private bool cashMashine1;
        private bool cashMashine2;

        public Bank()
        {
            //we set total
            TotalAmount = 10000;
        }

        /// <summary>
        /// method that withdraws money from bank amount
        /// if the amount of money that we want to withdraw is bigger than total amount on bank account
        /// method returns 0
        /// </summary>
        /// <param name="ammountToWithdraw">amount of money that we want to witdraw</param>
        /// <returns></returns>
        int Withdraw(int ammountToWithdraw)
        {
            if (TotalAmount < 0)
            {
                throw new Exception("Crvena zona");
            }

            if (TotalAmount >= ammountToWithdraw)
            {

                Console.WriteLine("Stanje na racunu u banci: {0}", TotalAmount);
                Console.WriteLine("Iznos koji klijent podize: {0}", ammountToWithdraw);
                TotalAmount -= ammountToWithdraw;
                Console.WriteLine("Stanje na racunu nakon podizanja: {0}\n", TotalAmount);
                return ammountToWithdraw;
            }
            else
            {
                return 0; // Transakcija odbijena
            }


        }

        /// <summary>
        /// function that simulates work of first cash mashine
        /// it waits if second cash mashine is active
        /// </summary>
        /// <param name="ammountToWithdraw"></param>
        public void DoTransactionsOnCashMashine1(int ammountToWithdraw)
        {
            lock (thisLock)
            {
                
                if (cashMashine2)
                {
                    //method will wait to execute if cashMashine2 is active
                    while (cashMashine2)
                    {
                        Thread.Sleep(25);
                    }
                    //withdrawing of money from the account
                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata1",
                        Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine1 = true;
                    int withdrawnAmount = Withdraw(ammountToWithdraw);

                    //checks if money was withdrawn
                    if (withdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke\n");
                    }
                    Thread.Sleep(1000);
                    cashMashine1 = false;

                }
                else
                {
                    //this part executes if  first cashMashine2 is not active
                    
                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata1",
                     Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine1 = true;
                    int withdrawnAmount = Withdraw(ammountToWithdraw);
                    if (withdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke\n");
                    }
                    Thread.Sleep(1000);
                    cashMashine1 = false;
                }
            }
        }

        public void DoTransactionsOnCashMashine2(int ammountToWithdraw)
        {
            lock (thisLock)
            {
                if (cashMashine1)
                {
                    //method waits to execute if the first  cashmashine is active
                    while (cashMashine1)
                    {
                        Thread.Sleep(25);
                    }
                    //money is withdrawn from the bank account
                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata2",
                     Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine2 = true;
                    int witdrawnAmount = Withdraw(ammountToWithdraw);
                    //checks if money was withdrown
                    if (witdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke\n");
                    }
                    Thread.Sleep(1000);
                    cashMashine2 = false;

                }
                else
                {
                    // this part executes if  cashMashine1 is not active
                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata2" +
                        "",
                     Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine2 = true;
                    int witdrawnAmount = Withdraw(ammountToWithdraw);
                    if (witdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke\n");
                    }
                    cashMashine2 = false;
                }
            }
        }
    }
}
