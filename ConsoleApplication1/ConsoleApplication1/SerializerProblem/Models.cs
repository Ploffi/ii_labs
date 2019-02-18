using System.Linq;

namespace ConsoleApplication1.SerializerProblem
{
    public class Input
    {
        public int K { get; set; }
        public decimal[] Sums { get; set; }
        public int[] Muls { get; set; }

        public override bool Equals(object obj)
        {
            var inp = obj as Input;

            if (inp == null)
                return false;
            return inp.Muls.SequenceEqual(Muls) &&
                   inp.Sums.SequenceEqual(Sums) &&
                   inp.K.Equals(K);
        }
    }

    public class Output
    {
        public decimal SumResult { get; set; }
        public int MulResult { get; set; }
        public decimal[] SortedInputs { get; set; }
    }
}