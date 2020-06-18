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
        int totalAmount = 10000;
        bool cashMashine1;
        bool cashMashine2;
       
        
        int Withdraw(int ammountToWithdraw)
        {
            if (totalAmount < 0)
            {
                throw new Exception("Usli ste u crveno");
            }
            lock (thisLock)
            {
                if (totalAmount >= ammountToWithdraw)
                {

                    Console.WriteLine("Stanje na racunu u banci: " + totalAmount);
                    Console.WriteLine("Iznos koji podizete: " + ammountToWithdraw);
                    totalAmount -= ammountToWithdraw;
                    Console.WriteLine("Stanje na racunu nakon podizanja: " + totalAmount);
                    return ammountToWithdraw;
                }
                else
                {
                    return 0; // Transakcija odbijena
                }
            }

        }
       public void DoTransactionsOnCahsMashine1(int ammountToWithdraw)
       {
            if (cashMashine2)
            {
                while (cashMashine2)
                {
                    Thread.Sleep(25);
                }
                cashMashine1 = true;
                Withdraw(ammountToWithdraw);
                Thread.Sleep(1000);
                cashMashine1 = false;

            }

        }

        public void DoTransactionsOnCahsMashine2(int ammountToWithdraw)
        {
            if (cashMashine1)
            {
                while (cashMashine1)
                {
                    Thread.Sleep(25);
                }
                cashMashine2 = true;
                Withdraw(ammountToWithdraw);
                Thread.Sleep(1000);
                cashMashine2 = false;

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
                 Thread t = new Thread(() => bank.DoTransactionsOnCahsMashine1(rnd.Next(10,10000)));
                t.Name = String.Format("Klijent{0}_Bankomat1",i+1);
                cashMashine1Clients.Enqueue(t);
              }
            foreach (var item in cashMashine1Clients)
            {
                Console.WriteLine(item.Name);
            }
             Console.ReadLine();
          }
        }

    }
 