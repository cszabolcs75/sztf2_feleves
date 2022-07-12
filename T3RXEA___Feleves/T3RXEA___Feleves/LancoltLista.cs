using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3RXEA___Feleves
{
    public class ListaElem<T> where T : IComparable
    {
        public T Tartalom { get; set; }

        public ListaElem<T> Kovetkezo { get; set; }
    } 


    public class LancoltLista<T>: IEnumerable<T> where T :  IComparable
    {

        private ListaElem<T> fej;

        public int counter { get; set; }
        public ListaElem<T> Fej { get { return fej; } }

        public void Beszuras(T tartalom)
        {
            ListaElem<T> ujelem = new ListaElem<T>();
            ujelem.Tartalom = tartalom;
            ListaElem<T> p = fej;
            ListaElem<T> e = null;
            counter++;
            while (p != null && p.Tartalom.CompareTo(tartalom) < 0)
            {
                e = p;
                p = p.Kovetkezo;
                
            }

            if (e == null)
            {
                ujelem.Kovetkezo = fej;
                fej = ujelem;
            }
            else
            {
                ujelem.Kovetkezo = p;
                e.Kovetkezo = ujelem;
            }
        }

        public void Torles(T elem)
        {
            ListaElem<T> p = fej;
            ListaElem<T> e = null;
            while (p != null)
            {
                if (p.Tartalom.Equals(elem))
                {
                    if (e == null)
                    {
                        fej = p.Kovetkezo;
                    }
                    else
                    {
                        e.Kovetkezo = p.Kovetkezo;
                    }
                }
                else
                {
                    e = p;
                }
                p = p.Kovetkezo;
            }
        }
      
        public T Kereses(int sorszam)
        {
            ListaElem<T> p;
            p = fej;
            int seged = 0;
            while (seged != sorszam)
            {
                seged++;
                if (p.Kovetkezo != null)
                {
                    p = p.Kovetkezo;
                }

            }
            if (seged == 0)
            {
                return fej.Tartalom;
            }
            else
            {
                return p.Tartalom;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListaBejaro<T>(fej);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ListaBejaro<T>(fej);
        }

        class ListaBejaro<T> : IEnumerator<T> where T :  IComparable
        {
            ListaElem<T> fej;
            ListaElem<T> aktualis;
            public ListaBejaro(ListaElem<T> fej)
            {
                this.fej = fej;
                this.aktualis = new ListaElem<T>();
                this.aktualis.Kovetkezo = fej;
            }
            public object Current { get { return fej; } }

            public bool MoveNext()
            {
                aktualis = aktualis.Kovetkezo;
                return aktualis != null;
            }

            public void Reset()
            {
                aktualis = new ListaElem<T>();
                aktualis.Kovetkezo = fej;
            }
            public void Dispose()
            {
                
            }

            T IEnumerator<T>.Current => aktualis.Tartalom;

        }
    }
}
