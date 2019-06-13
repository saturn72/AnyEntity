using AnyEntity;

namespace Sample.Models
{
    public class TestClass1 : AnyEntityModelBase<object>
    {
        public object Id { get; set; }
        public int NumericValue { get; set; }
        public string StringValue { get; set; }
    }
}