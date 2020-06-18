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
        Random rnd = new Random();
        
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
       
    }
        class Program
        {
            static void Main(string[] args)
            {
                Console.ReadLine();
            }
        }

    }
 