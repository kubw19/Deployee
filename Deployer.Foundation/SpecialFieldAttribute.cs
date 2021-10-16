using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Foundation
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class SpecialFieldAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly SpecialFieldEnum _fieldType;
        readonly string _value;

        // This is a positional argument
        public SpecialFieldAttribute(SpecialFieldEnum type, string value = "")
        {
            _fieldType = type;
            _value = value;
        }

        public SpecialFieldEnum FieldType
        {
            get { return _fieldType; }
        }

        public string Value
        {
            get { return _value; }
        }

        public static SpecialTypeDto GetSpecialType(PropertyInfo propertyInfo)
        {
            var attr = propertyInfo.GetCustomAttribute(typeof(SpecialFieldAttribute)) as SpecialFieldAttribute;

            if (attr != null)
            {
                return new SpecialTypeDto
                {
                    FieldType = attr.FieldType.ToString(),
                    Value = attr.Value

                };
            }
            return null;

        }

    }

    public class SpecialTypeDto
    {
        public string FieldType { get; set; }
        public string Value { get; set; }
    }

    public enum SpecialFieldEnum
    {
        Artifact,
        TargetRole
    }
}
