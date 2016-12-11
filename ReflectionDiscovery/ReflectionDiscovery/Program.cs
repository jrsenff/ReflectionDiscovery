using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReflectionDiscovery
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("\nBegin Reflection Discovery\n");
            Console.WriteLine("Select a .DLL file containing an assembly manifest...");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Multiselect = false;
            ofd.Title = "Select .DLL Assembly";
            ofd.Filter = "Dynamic Link Library | *.dll";
            ofd.ShowDialog();
            string assembly = ofd.FileName;
            Console.WriteLine("\nAssembly chosen: \n");
            Console.WriteLine(assembly + "\n");

            try
            {
                Assembly a = Assembly.LoadFrom(assembly);
                Console.WriteLine("\nAssembly name = " + a.GetName());

                Type[] tarr = a.GetTypes();
                BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public |
                                     BindingFlags.Static | BindingFlags.Instance;

                foreach (Type t in tarr)
                {
                    Console.WriteLine("  Type name = " + t.Name + "\n");
                    MemberInfo[] members = t.GetMembers(flags);
                    foreach (MemberInfo mi in members)  // fields, methods, ctors, etc.
                    {
                        if (mi.MemberType == MemberTypes.Field)
                            Console.WriteLine("  (Field) member name = " + mi.Name);
                    } 

                    MethodInfo[] miarr = t.GetMethods();  // public only
                    foreach (MethodInfo mi in miarr)
                    {
                        Console.WriteLine("   Method name = " + mi.Name);
                        Console.WriteLine("    Return type = " + mi.ReturnType);
                        ParameterInfo[] piarr = mi.GetParameters();
                        foreach (ParameterInfo pi in piarr)
                        {
                            Console.WriteLine("      Parameter name = " + pi.Name);
                            Console.WriteLine("      Parameter type = " + pi.ParameterType + "\n");
                        }
                    } 
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nFatal error: " + ex.Message);
                Console.WriteLine("  Error data: " + ex.Data);
                Console.WriteLine("  Error source: " + ex.Source);
            }
            Console.WriteLine("\n\nDone! Press Enter to exit...");
            Console.ReadLine();
        } 
    }
}
