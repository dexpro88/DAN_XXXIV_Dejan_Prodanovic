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
        int totalAmount = 100000;
        bool cashMashine1;
        bool cashMashine2;
       
        
        int Withdraw(int ammountToWithdraw)
        {
            if (totalAmount < 0)
            {
                throw new Exception("Crvena zona");
            }
            
                if (totalAmount >= ammountToWithdraw)
                {

                    Console.WriteLine("Stanje na racunu u banci: {0}", totalAmount);
                    Console.WriteLine("Iznos koji klijent podize: {0}", ammountToWithdraw);
                    totalAmount -= ammountToWithdraw;
                    Console.WriteLine("Stanje na racunu nakon podizanja: {0}\n", totalAmount);
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
        class Program
        {
            static void Main(string[] args)
            {
              Random rnd = new Random();
              Bank bank = new Bank();
              Queue<Thread> cashMashine1Clients = new Queue<Thread>();
              Queue<Thread> cashMashine2Clients = new Queue<Thread>();

              for (int i = 0; i < 10; i++)
              {
                 Thread t1 = new Thread(() => bank.DoTransactionsOnCashMashine1(rnd.Next(10,10000)));
                t1.Name = String.Format("Klijent{0}_Bankomat1",i+1);
                cashMashine1Clients.Enqueue(t1);
                Thread t2 = new Thread(() => bank.DoTransactionsOnCashMashine2(rnd.Next(10, 10000)));
                t2.Name = String.Format("Klijent{0}_Bankomat2", i + 1);
                cashMashine2Clients.Enqueue(t2);
            }
            for (int i = 0; i < 10; i++)
            {
              cashMashine1Clients.Dequeue().Start();
                cashMashine2Clients.Dequeue().Start();
            }
            
            Console.ReadLine();
          }
        }

    }
 