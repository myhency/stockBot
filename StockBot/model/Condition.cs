namespace StockBot.model
{
    public class Condition
    {
        public int index { get; set; }
        public string name { get; set; }

        public Condition(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }
}
