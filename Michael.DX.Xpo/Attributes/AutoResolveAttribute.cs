using System;

namespace DevExpress.Xpo
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true) ]
    public class AutoResolveAttribute : Attribute
    {
        public string FieldName { get; private set; } 

        public AutoResolveAttribute(string fieldname):base()
        {
            FieldName= fieldname;
        }
    }
}
