namespace puzzle
{
    class ElementZero
    {
        public static readonly int Valoare = 0; //flag cu valoarea unde e blank-ul
        int _rand;
        int _coloana;

        public ElementZero(int rand, int coloana)
        {
            _rand = rand;
            _coloana = coloana;
        }

        public int Rand
        {
            get { return _rand; }
        }

        public int Coloana
        {
            get { return _coloana; }
        }
    }
}
