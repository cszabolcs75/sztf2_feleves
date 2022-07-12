using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3RXEA___Feleves
{
    public class Konferencia: IComparable
    {
        public string nev;
        public Nyelvek munkaNyelve;
        public int fizetes;
        public Konferencia(string nev, int fizetes, string munkaNyelve)
        {
            this.nev = nev;
            this.fizetes = fizetes;
            this.munkaNyelve = (Nyelvek)Enum.Parse(typeof(Nyelvek), munkaNyelve);
        }
        public override string ToString()
        {
            return nev;
        }
        public int CompareTo(object obj)
        {
            return nev.CompareTo((obj as Konferencia).nev);
        }
    }
}
