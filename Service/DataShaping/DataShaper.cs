using Contracts;
using Entities.Models;
using System.Dynamic;
using System.Reflection;

namespace Service.DataShaping
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fields)
        {
            var requiredProperties = GetRequiredProperties(fields);
            return (IEnumerable<ShapedEntity>)FetchData(entities, requiredProperties);
        }

        public ShapedEntity ShapeData(T entity, string fields)
        {
            var requiredProperties = GetRequiredProperties(fields);
            return FetchDataForEntity(entity, requiredProperties);
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fields)
        {
            var requiredProperties = new List<PropertyInfo>();
            if (!string.IsNullOrWhiteSpace(fields))
            {
                var fieldsList = fields.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var field in fieldsList)
                {
                    var property = Properties.FirstOrDefault(a => a.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                    if (property == null)
                    {
                        continue;
                    }
                    requiredProperties.Add(property);
                }
            }
            else
            {
                requiredProperties = Properties.ToList();
            }
            return requiredProperties;
        }

        private ShapedEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requieredProperties)
        {
            var shapedObject = new ShapedEntity();
            foreach (var property in requieredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.expandoObject.TryAdd(property.Name, objectPropertyValue);
            }
            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (Guid)objectProperty.GetValue(entity);
            return shapedObject;
        }

        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ExpandoObject>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject.expandoObject);
            }
            return shapedData;
        }
    }
}
