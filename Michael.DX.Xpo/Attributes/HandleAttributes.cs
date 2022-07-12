using DevExpress.ExpressApp;
using DevExpress.Xpo;
using Michael.DX.Container;
using System.Linq;
using System.Reflection;

namespace Michael.Xpo
{
    internal static class HandleAttributes
    {

        internal static void Fix(XPBaseObject xPBaseObject)
        {
            IContainerProvider container = xPBaseObject.GetContainerProvider();
            xPBaseObject.GetType().GetCustomAttributes(true).ToList().ForEach(x =>
            {
                if (x is AutoResolveAttribute resolve)
                {
                    var fieldinfo = xPBaseObject
                            .GetType()
                            .GetField(resolve.FieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (fieldinfo != null)
                    {
                        var type = fieldinfo.FieldType;
                        fieldinfo.SetValue(xPBaseObject, container.Resolve(type));
                    }
                    else
                    {
                        var prop = xPBaseObject.GetType().GetProperty(resolve.FieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        prop?.SetValue(xPBaseObject, container.Resolve(prop.PropertyType));
                    }
                }
                
            });
        }
    }
}
