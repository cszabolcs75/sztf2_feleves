using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3RXEA___Feleves
{
    class TolmacsCeg<T, K> where T : IComparable
         where K : IComparable
    {
        public List<T> tolmacsok;
        public List<K> konferenciak;

        public Tolmács[] tolmacstomb;
        public Konferencia[] konferenciatomb;

        public TolmacsCeg()
        {
            this.tolmacsok = new List<T>();
            this.konferenciak = new List<K>();
            tolmacstomb = new Tolmács[0];
            konferenciatomb = new Konferencia[0];
        }
        public void tolmacsfeltolto(T elem)
        {
            this.tolmacsok.Add(elem);
        }
        public void konferenciafeltolto(K elem)
        {
            this.konferenciak.Add(elem);
        }
        public void Jelentkezes(BinarisKeresoFa<Konferencia, Tolmács> fa, List<Tolmács> tolmacsok, List<Konferencia> konferenciak)
        {
            for (int i = 0; i < konferenciak.Count; i++)
            {
                for (int j = 0; j < tolmacsok.Count; j++)
                {
                    if (konferenciak[i].munkaNyelve == tolmacsok[j].nyelvTudás)
                    {
                        fa.UjTolmacsFelvetele(konferenciak[i], tolmacsok[j]);
                    }
                }
            }
        }
        public Tolmács[][] FureszfogasLetrehozo(BinarisKeresoFa<Konferencia, Tolmács> fa, List<Tolmács> tolmacsok, List<Konferencia> konferenciak)
        {
            Tolmács[][] fureszfogas = new Tolmács[konferenciak.Count][];
            for (int j = 0; j < fureszfogas.Length; j++)
            {
                if (fa.Kereses2(konferenciak[j]) != null)
                {
                    LancoltLista<Tolmács> seged = fa.Kereses2(konferenciak[j]);
                    fureszfogas[j] = new Tolmács[seged.counter];
                    for (int k = 0; k < fureszfogas[j].Length; k++)
                    {
                        fureszfogas[j][k] = seged.Kereses(k);
                    }
                }
                else
                {
                    fureszfogas[j] = new Tolmács[0];
                }
            }
            return fureszfogas;
        }
        public void BTS(Tolmács[][] r, int szint, Tolmács[] e, ref bool van, Tolmács[] OPT)
        {
            int i = -1;
            while (i < r[szint].Length - 1)
            {
                i++;
                if (Fk(szint, r[szint][i], e))
                {
                    e[szint] = r[szint][i];
                    if (szint == r.Length - 1)
                    {
                        if (!van || Árak(e)> Árak(OPT))
                        {
                            for (int j = 0; j < e.Length; j++)
                            {
                                OPT[j] = e[j];
                            }
                        }


                        van = true;
                    }
                    else
                    {
                        BTS(r, szint + 1, e, ref van, OPT);
                    }
                }
            }
        }

        public int Árak(Tolmács[] e)
        {
            int sum = 0;
            for (int i = 0; i < e.Length; i++)
            {
                sum += e[i].ár;
            }

            return sum;
        }
        


        public bool Fk(int szint, Tolmács tolmacs, Tolmács[] e)
        {
            bool ok = true;
            for (int i = 0; i < szint; i++)
            {
                if (e[i] == tolmacs)
                {
                    ok = false;
                }
            }
            return ok;
        }
        public void VisszalepesesKereses(BinarisKeresoFa<Konferencia, Tolmács> fa, List<Tolmács> tolmacsok, List<Konferencia> konferenciak)
        {
            Tolmács[][] r = FureszfogasLetrehozo(fa, tolmacsok, konferenciak);
            Tolmács[] e = new Tolmács[r.Length];
            Tolmács[] OPT = new Tolmács[r.Length];
            bool van = false;
            BTS(r, 0, e, ref van, OPT);
            for (int i = 0; i < OPT.Length; i++)
            {
                Console.WriteLine($"{i}. szint megoldása: {OPT[i]}");
            }
            for (int i = 0; i < OPT.Length; i++)
            {
                Kioszto(fa, OPT[i], konferenciak[i]);
            }
        }
        public void Kioszto(BinarisKeresoFa<Konferencia, Tolmács> fa, Tolmács tolmacs, Konferencia konferencia)
        {

            try
            {
                LancoltLista<Tolmács> seged = fa.Kereses2(konferencia);

                int i = 0;
                while (i < seged.counter)
                {
                    if (seged.Kereses(i) != tolmacs)
                    {
                        fa.TolmacsEltavolito(konferencia, seged.Kereses(i));
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
