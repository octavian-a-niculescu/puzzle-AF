using System;
using System.Collections.Generic;
using System.Text;

namespace puzzle
{
    class Stare
    {
        int[,] _elemente;
        ElementZero _elementZero;
        string _actiune = null;
        Stare _stareAnterioara;
        int _cost;

        public Stare(int[,] elemente, Stare stareAnterioara, string actiune)
        {
            _elemente = (int[,])elemente.Clone();
            _stareAnterioara = stareAnterioara;
            _actiune = actiune;
            _elementZero = GetElementZero();
        }

        private ElementZero GetElementZero()
        {
            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    if (_elemente[i, j] == ElementZero.Valoare)
                    {
                        return new ElementZero(i, j);
                    }
                }
            }

            throw new Exception(String.Format("Lipseste elementul cu valoare {0}", ElementZero.Valoare));
        }

        public bool AreSolutie()
        {
            int nrInversiuni = 0;
            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    for (int p = i; p < Randuri; p++)
                    {
                        for (int t = (p == i ? j + 1 : 0); t < Coloane; t++)
                        {
                            if (_elemente[i, j] > _elemente[p, t] && _elemente[i, j] != 0 && _elemente[p, t] != 0)
                            {
                                nrInversiuni++;
                            }
                        }
                    }
                }
            }
            if (Coloane % 2 == 1 && nrInversiuni % 2 == 0 || (Coloane % 2 == 0 && (((Coloane - _elementZero.Coloana) % 2 == 1) == (nrInversiuni % 2 == 0))))
            {
                return true;
            }
            return false;
        }

        public bool EsteSolutie()
        {
            int prevVal = 0;

            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    int val = _elemente[i, j];

                    if (val > prevVal
                        || (val == ElementZero.Valoare && i == Randuri - 1 && j == Coloane - 1))
                    {
                        prevVal = val;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        private Stare GetStareNoua(int offsetRand, int offsetColoana, string actiune)
        {
            var elementeNoi = (int[,])_elemente.Clone();

            int swap = elementeNoi[_elementZero.Rand + offsetRand, _elementZero.Coloana + offsetColoana];
            elementeNoi[_elementZero.Rand + offsetRand, _elementZero.Coloana + offsetColoana] = elementeNoi[_elementZero.Rand, _elementZero.Coloana];
            elementeNoi[_elementZero.Rand, _elementZero.Coloana] = swap;

            return new Stare(elementeNoi, this, actiune);
        }

        public List<Stare> GetStariUrmatoare()
        {
            var stari = new List<Stare>();

            // Jos
            if (_elementZero.Rand < Randuri - 1)
            {
                var stareNoua = GetStareNoua(1, 0, "Jos");
                stari.Add(stareNoua);
            }

            // Dreapta
            if (_elementZero.Coloana < Coloane - 1)
            {
                var stareNoua = GetStareNoua(0, 1, "dreapta");
                stari.Add(stareNoua);
            }

            // Stanga
            if (_elementZero.Coloana > 0)
            {
                var stareNoua = GetStareNoua(0, -1, "stanga");
                stari.Add(stareNoua);
            }

            // Sus
            if (_elementZero.Rand > 0)
            {
                var stareNoua = GetStareNoua(-1, 0, "sus");
                stari.Add(stareNoua);
            }

            return stari;
        }

        public string Print()
        {
            var sb = new StringBuilder();
            foreach(int val in _elemente)
            {
                sb.Append(Convert.ToString(val));
                sb.Append(" ");
            }
            sb.Append("\n");
            sb.Append(_actiune);
            sb.Append("\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (int val in _elemente)
            {
                sb.Append(Convert.ToString(val));
                sb.Append('_');
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public int Randuri
        {
            get { return _elemente.GetLength(0); }
        }

        public int Coloane
        {
            get { return _elemente.GetLength(1); }
        }

        public Stare StareAnterioara
        {
            get { return _stareAnterioara; }
        }

        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
    }
}
