namespace CustomHelpers
{

    public class Pair<T1, T2>
    {

        public Pair(T1 first, T2 second)
        {
            this.First =first;
            this.Second= second;
        }

        public T1 First { get; set; }
        public T2 Second { get; set; }

    }

}