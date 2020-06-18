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

            for (int i = 0; i < firstMashineQueueLength; i++)
            {
                 Thread t = new Thread(() => bank.DoTransactionsOnCashMashine1(rnd.Next(10,10000)));
                t.Name = String.Format("Klijent{0}_Bankomat1",i+1);
                cashMashine1Clients.Enqueue(t);
               
            }
            for (int i = 0; i < secondMashineQueueLength; i++)
            {
                Thread t = new Thread(() => bank.DoTransactionsOnCashMashine2(rnd.Next(10, 10000)));
                t.Name = String.Format("Klijent{0}_Bankomat2", i + 1);
                cashMashine2Clients.Enqueue(t);
            }

            StartThreads(cashMashine1Clients, cashMashine2Clients);


            Console.ReadLine();
          }
        /// <summary>
        /// method that takes int input from the keyboard
        /// it disables user to input invalid values
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// method that starts threads from 2 queues (one is for first cash mashine,
        /// one for second cash mashine). it starts threads from both queues alternately
        /// </summary>
        /// <param name="cashMashine1Clients"></param>
        /// <param name="cashMashine2Clients"></param>
        static void StartThreads(Queue<Thread> cashMashine1Clients ,Queue<Thread> cashMashine2Clients)
        {
            if (cashMashine2Clients.Count < cashMashine1Clients.Count)
            {
                for (int i = 0; i < cashMashine2Clients.Count; i++)
                {
                    cashMashine2Clients.Dequeue().Start();
                    cashMashine1Clients.Dequeue().Start();
                }
                while (cashMashine1Clients.Count != 0)
                {
                    cashMashine1Clients.Dequeue().Start();
                }
            }
            else
            {
                for (int i = 0; i < cashMashine1Clients.Count; i++)
                {
                    cashMashine2Clients.Dequeue().Start();
                    cashMashine1Clients.Dequeue().Start();
                }
                while (cashMashine2Clients.Count != 0)
                {
                    cashMashine2Clients.Dequeue().Start();
                }
            }
        }
      }
      
    }
 