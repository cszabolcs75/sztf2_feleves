using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3RXEA___Feleves
{
    class BinarisKeresoFa<K, T> where K : IComparable where T : class, IComparable
    {
        FaElem gyökér;
        public class FaElem
        {
            public K Kulcs { get; set; }
            public LancoltLista<T> Érték;
            public FaElem Bal;
            public FaElem Jobb;


            public FaElem(K kulcs)
            {
                Kulcs = kulcs;
                Érték = new LancoltLista<T>();
            }

            public FaElem(K kulcs, LancoltLista<T> érték, FaElem bal, FaElem jobb)
            {
                Kulcs = kulcs;
                Érték = érték;
                Bal = bal;
                Jobb = jobb;
            }

        }

        public void UjTolmacsFelvetele(K kulcs, T érték)
        {
            UjTolmacsFelvetele(ref gyökér, kulcs, érték);
        }
        private void UjTolmacsFelvetele(ref FaElem p, K kulcs, T érték)
        {
            if (p != null)
            {
                if (p.Kulcs.CompareTo(kulcs) > 0)
                {
                    UjTolmacsFelvetele(ref p.Bal, kulcs, érték);
                }      
                else if (p.Kulcs.CompareTo(kulcs) < 0)
                {
                    UjTolmacsFelvetele(ref p.Jobb, kulcs, érték);
                }
                else
                {
                    p.Érték.Beszuras(érték);
                }
            }
        }
        public void TolmacsEltavolito(K kulcs, T érték)
        {
            TolmacsEltavolito(ref gyökér, kulcs, érték);
        }
        private void TolmacsEltavolito(ref FaElem p, K kulcs, T érték)
        {
            if (p != null)
            {
                if (p.Kulcs.CompareTo(kulcs) > 0)
                {
                    TolmacsEltavolito(ref p.Bal, kulcs, érték);
                }
                else if (p.Kulcs.CompareTo(kulcs) < 0)
                {
                    TolmacsEltavolito(ref p.Jobb, kulcs, érték);
                }
                else
                {
                    p.Érték.Torles(érték);
                }
                
            }
        }

        public void UjKonferenciaFelvetele(K kulcs)
        {
            _UjKonferenciaFelvetele(ref gyökér, kulcs);
        }

        private void _UjKonferenciaFelvetele(ref FaElem p, K kulcs)
        {
            if (p == null)
            {
                p = new FaElem(kulcs);
            }
            else if (p.Kulcs.CompareTo(kulcs) < 0)
            {
                _UjKonferenciaFelvetele(ref p.Jobb, kulcs);
            }
            else if (p.Kulcs.CompareTo(kulcs) > 0)
            {
                _UjKonferenciaFelvetele(ref p.Bal, kulcs);
            }
            
        }
        public List<K> InOrder()
        {

            List<K> tmp = new List<K>();
            _InOrderBejaras(tmp, gyökér);
            return tmp;
        }
        private void _InOrderBejaras(List<K> tmp, FaElem p)
        {
            if (p != null)
            {
                _InOrderBejaras(tmp, p.Bal);
                tmp.Add(p.Kulcs);
                _InOrderBejaras(tmp, p.Jobb);
            }
        }
        public void InOrder2()
        {
            _InOrderBejaras2(gyökér);
        }
        private void _InOrderBejaras2(FaElem p)
        {
            if (p != null)
            {
                _InOrderBejaras2(p.Bal);
                if (p.Érték.Fej != null)
                {
                    Console.WriteLine(p.Kulcs.ToString() + " --- " + p.Érték.Fej.Tartalom.ToString());
                }
                else
                {
                    Console.WriteLine(p.Kulcs.ToString());
                }
                _InOrderBejaras2(p.Jobb);
            }
        }
        public T Kereses(K kulcs)
        {
            return _Kereses(gyökér, kulcs);
        }
        private T _Kereses(FaElem p, K kulcs)
        {

            if (p.Kulcs.Equals(kulcs))
            {
                return p.Érték.Fej.Tartalom;
            }
            else if (p.Kulcs.CompareTo(kulcs) < 0)
            {
                return _Kereses(p.Jobb, kulcs);
            }
            else if (p.Kulcs.CompareTo(kulcs) > 0)
            {
                return _Kereses(p.Bal, kulcs);
            }
            else
            {
                throw new KeyNotFoundException();
            }


        }
        public LancoltLista<T> Kereses2(K kulcs)
        {
            return _Kereses2(gyökér, kulcs);
        }
        private LancoltLista<T> _Kereses2(FaElem p, K kulcs)
        {

            if (p != null && p.Érték.Fej != null)
            {
                if (p.Kulcs.CompareTo(kulcs) < 0)
                {
                    return _Kereses2(p.Jobb, kulcs);
                }
                else if (p.Kulcs.CompareTo(kulcs) > 0)
                {
                    return _Kereses2(p.Bal, kulcs);
                }
                else
                {
                    return p.Érték;
                }
            }
                return null;
        }
    }
}
