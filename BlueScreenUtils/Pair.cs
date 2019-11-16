namespace Utils
{
    public class Pair<A, B>
    {
        public A First { get; set; }
        public B Second { get; set; }

        public Pair(A first, B second)
        {
            this.First = first;
            this.Second = second;
        }
    }
}