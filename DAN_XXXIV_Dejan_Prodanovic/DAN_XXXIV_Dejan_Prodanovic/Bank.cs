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
            TotalAmount = 10000;
        }
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
        public void DoTransactionsOnCashMashine1(int ammountToWithdraw)
        {
            lock (thisLock)
            {
                if (cashMashine2)
                {

                    while (cashMashine2)
                    {
                        Thread.Sleep(25);
                    }

                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata1",
                        Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine1 = true;
                    int withdrawnAmount = Withdraw(ammountToWithdraw);
                    if (withdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke");
                    }
                    Thread.Sleep(1000);
                    cashMashine1 = false;

                }
                else
                {
                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata1",
                     Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine1 = true;
                    int withdrawnAmount = Withdraw(ammountToWithdraw);
                    if (withdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke");
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

                    while (cashMashine1)
                    {
                        Thread.Sleep(25);
                    }
                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata2",
                     Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine2 = true;
                    int witdrawnAmount = Withdraw(ammountToWithdraw);
                    if (witdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke");
                    }
                    Thread.Sleep(1000);
                    cashMashine2 = false;

                }
                else
                {

                    Console.WriteLine("Klijent {0} pokusava da podigne {1} RSD sa bankomata2" +
                        "",
                     Thread.CurrentThread.Name, ammountToWithdraw);
                    cashMashine2 = true;
                    int witdrawnAmount = Withdraw(ammountToWithdraw);
                    if (witdrawnAmount == 0)
                    {
                        Console.WriteLine("Nema dovoljno novca na racunu banke");
                    }
                    cashMashine2 = false;
                }
            }
        }
    }
}
