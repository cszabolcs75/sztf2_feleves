using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3RXEA___Feleves
{
    class Program
    {

        static void teszt()
        {
            
            BinarisKeresoFa<Konferencia, Tolmács> fa = new BinarisKeresoFa<Konferencia, Tolmács>();
            TolmacsCeg<Tolmács, Konferencia> tolmacsceg = new TolmacsCeg<Tolmács, Konferencia>();
            FajlBeolvasas(tolmacsceg, fa);
            tolmacsceg.Jelentkezes(fa, tolmacsceg.tolmacsok, tolmacsceg.konferenciak);
            tolmacsceg.VisszalepesesKereses(fa, tolmacsceg.tolmacsok, tolmacsceg.konferenciak);
            fa.InOrder2();
        }
        static void Main(string[] args)
        {
            teszt();
            Console.ReadLine();
        }

        static void FajlBeolvasas(TolmacsCeg<Tolmács, Konferencia> tolmacsceg, BinarisKeresoFa<Konferencia, Tolmács> fa)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory());
            string[] txtfiles = Directory.GetFiles(path, "*.txt");
            string[] tolmacssorok = File.ReadAllLines(txtfiles[1]);
            string[] konferenciasorok = File.ReadAllLines(txtfiles[0]);
            tolmacsceg.tolmacstomb = new Tolmács[tolmacssorok.Length];
            tolmacsceg.konferenciatomb = new Konferencia[konferenciasorok.Length];
            for (int i = 0; i < tolmacssorok.Length; i++)
            {
                string[] tolmacssordarabok = tolmacssorok[i].Split('\t');
                string nev = tolmacssordarabok[0];
                int ar = int.Parse(tolmacssordarabok[1]);
                string nyelv = tolmacssordarabok[2];
                tolmacsceg.tolmacstomb[i] = new Tolmács(nev, ar, nyelv);
            }
            for (int i = 0; i < konferenciasorok.Length; i++)
            {
                string[] konferenciasordarabok = konferenciasorok[i].Split('\t');
                string nev = konferenciasordarabok[0];
                int ar = int.Parse(konferenciasordarabok[1]);
                string konferencianyelve = konferenciasordarabok[2];
                tolmacsceg.konferenciatomb[i] = new Konferencia(nev, ar, konferencianyelve);
            }
            for (int i = 0; i < tolmacsceg.tolmacstomb.Length; i++)
            {
                tolmacsceg.tolmacsfeltolto(tolmacsceg.tolmacstomb[i]);
            }
            for (int i = 0; i < tolmacsceg.konferenciatomb.Length; i++)
            {
                tolmacsceg.konferenciafeltolto(tolmacsceg.konferenciatomb[i]);
                fa.UjKonferenciaFelvetele(tolmacsceg.konferenciatomb[i]);
            }
            ;
        }
    }
}
