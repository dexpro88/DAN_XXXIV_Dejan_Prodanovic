using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XXXIV_Dejan_Prodanovic
{
  
        class Program
        {
            static void Main(string[] args)
            {
              Random rnd = new Random();
              Bank bank = new Bank();
              Queue<Thread> cashMashine1Clients = new Queue<Thread>();
              Queue<Thread> cashMashine2Clients = new Queue<Thread>();

              Console.WriteLine("Unesite broj klijenata koji podizu novac sa prvog bankomata:");
              int firstMashineQueueLength = IntInput();
              Console.WriteLine("Unesite broj klijenata koji podizu novac sa drugog bankomata:");
              int secondMashineQueueLength = IntInput();

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

          static int IntInput()
          {
            bool succes=false;
            int value;
            do
            {
                succes = Int32.TryParse(Console.ReadLine(),out value);
                if (!succes || value <=0)
                {
                    Console.WriteLine("Nevalidan unos. Unesite pozitivan broj");
                }
            } while (!succes || value <= 0);
            return value;
          }
       }

    }
 