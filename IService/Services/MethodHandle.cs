using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IService.Services
{
    public class MethodHandle
    {
        public static object CallMethod(string code, string methodName, params object[] arguments)
        {
            PropertyInfo ?propertyInfo = typeof(System.DB).GetProperty($"_{code}");
            if (propertyInfo != null)
            {
                object ?collectionObject = propertyInfo.GetValue(null);

                if (collectionObject is BsonData.Collection)
                {
                    // Tìm phương thức cụ thể trong BsonData.Collection
                    MethodInfo ?method = typeof(BsonData.Collection).GetMethods()
                        .FirstOrDefault(m => m.Name == methodName
                                             && m.GetParameters().Length == arguments.Length
                                             && m.GetParameters().Select((p, i) => p.ParameterType.IsAssignableFrom(arguments[i].GetType())).All(x => x));

                    if (method != null)
                    {
                        // Gọi phương thức với đối số
                        object ?result = method.Invoke(collectionObject, arguments);

                        // Nếu phương thức là void, trả về true để chỉ ra rằng nó đã được thực thi thành công
                        if (method.ReturnType == typeof(void))
                        {
                            return true;
                        }
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Không tìm thấy phương thức {methodName} phù hợp trong BsonData.Collection.");
                    }
                }
                else
                {
                    Console.WriteLine($"Đối tượng _{code} không phải là BsonData.Collection.");
                }
            }
            else
            {
                Console.WriteLine($"Thuộc tính _{code} không tồn tại trong lớp DB.");
            }

            return null;
        }
    }
}
