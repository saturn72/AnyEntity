using AnyEntity;

namespace Sample.Models
{
    public class TestClass2 : IAnyEntityModelBase<string>
    {
        public string Id { get; set; }
        public int NumericValue { get; set; }
        public string StringValue1 { get; set; }
        public string StringValue2 { get; set; }
    }
}