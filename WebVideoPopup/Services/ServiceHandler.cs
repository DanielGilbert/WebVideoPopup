using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebVideoPopup.Extensions;
using WebVideoPopup.Services.Interfaces;

namespace WebVideoPopup.Services
{
    public static class ServiceHandler
    {
        private static IEnumerable<Type> GetAllServices(Assembly asm)
        {
            var it = typeof(IWebVideoService);
            return asm.GetLoadableTypes().Where(it.IsAssignableFrom).ToList();
        }

        public static IWebVideoService GetCorrespondingService(Assembly asm, string url)
        {
List<IWebVideoService> instances = (from assembly in AppDomain.CurrentDomain.GetAssemblies().ToList()
                 from type in assembly.GetTypes()
                 where !type.IsAbstract &&
                       type.IsClass &&
                       type.IsPublic &&
                       !type.IsGenericType &&
                       typeof(IWebVideoService).IsAssignableFrom(type)
                 let ctor = type.GetConstructor(Type.EmptyTypes)
                 where ctor != null && ctor.IsPublic
                 select (IWebVideoService)Activator.CreateInstance(type))
                .ToList();

            foreach (IWebVideoService entry in instances)
            {
                if (entry.CanHandle(url))
                {
                    return entry;
                }
            }
            return null;
        }
    }
}
