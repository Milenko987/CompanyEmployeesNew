using System.Dynamic;

namespace Entities.Models
{
    public class ShapedEntity
    {
        public Guid Id { get; set; }
        public ExpandoObject expandoObject { get; set; }

        public ShapedEntity()
        {
            expandoObject = new ExpandoObject();
        }
    }
}
