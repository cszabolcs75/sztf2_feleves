using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T3RXEA___Feleves
{
    public enum Nyelvek
    {
        Angol, Német, Magyar, Szlovák, Francia, Spanyol, Olasz
    }
    public class Tolmács : INyelviMunka, IComparable
    {
        public string nev;
        public int ár;
        public Nyelvek nyelvTudás;
        public Tolmács(string nev, int ár, string nyelvTudás)
        {
            this.nev = nev;
            this.ár = ár;
            this.nyelvTudás = (Nyelvek)Enum.Parse(typeof(Nyelvek), nyelvTudás);
        }
        public override string ToString()
        {
            return nev;
        }
        string INyelviMunka.Nev()
        {
            return nev;
        }
        public bool Nyelvismeret(string nyelv)
        {
            return (nyelvTudás.ToString() == nyelv);
        }
        int INyelviMunka.Ár(int ido)
        {
            return this.ár * ido;
        }

        public int CompareTo(object obj)
        {
            return ár.CompareTo((obj as Tolmács).ár);
        }
    }
}
