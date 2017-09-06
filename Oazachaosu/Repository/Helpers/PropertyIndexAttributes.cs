using System;
using System.Reflection;

namespace Repository.Helpers {
  [AttributeUsage(AttributeTargets.Property, Inherited = true)]
  public class PropertyIndexAttribute : Attribute {
    public int Index { get; private set; }
    public PropertyIndexAttribute(int index) {
      Index = index;
    }

    public static int GetPropertyIndex(Type type, string name) {
      PropertyInfo property = type.GetProperty(name);
      if (property == null) {
        Console.WriteLine("ERROR - Nie znaleziono właściwości");
        return -1;
      }
      PropertyIndexAttribute attribute = property.GetCustomAttribute<PropertyIndexAttribute>();
      if (attribute == null) {
        Console.WriteLine("ERROR - Nie znaleziono atrybutu");
        return -1;
      }
      return attribute.Index;
    }
  }
}
