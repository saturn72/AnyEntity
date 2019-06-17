using AnyEntity;

namespace Sample.Models
{
    public class TestClass1 : IAnyEntityModelBase<string>
    {
        public string Id { get; set; }
        public int NumericValue { get; set; }
        public string StringValue { get; set; }
    }
}