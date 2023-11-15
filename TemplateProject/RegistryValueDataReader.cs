using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject
{
    public class RegistryValueDataReader
    {
        public enum RegistryHive
        {
            Wow64,
            Wow6432
        }

        private static readonly int KEY_WOW64_32KEY = 0x200;
        private static readonly int KEY_WOW64_64KEY = 0x100;

        private static readonly UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr)0x80000002;

        private static readonly int KEY_QUERY_VALUE = 0x1;

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyEx")]
        static extern int RegOpenKeyEx(
                    UIntPtr hKey,
                    string subKey,
                    uint options,
                    int sam,
                    out IntPtr phkResult);


        [DllImport("advapi32.dll", SetLastError = true)]
        static extern int RegQueryValueEx(
                    IntPtr hKey,
                    string lpValueName,
                    int lpReserved,
                    out uint lpType,
                    IntPtr lpData,
                    ref uint lpcbData);

        private static int GetRegistryHiveKey(RegistryHive registryHive)
        {
            return registryHive == RegistryHive.Wow64 ? KEY_WOW64_64KEY : KEY_WOW64_32KEY;
        }

        private static UIntPtr GetRegistryKeyUIntPtr(RegistryKey registry)
        {
            if (registry == Registry.LocalMachine)
            {
                return HKEY_LOCAL_MACHINE;
            }

            return UIntPtr.Zero;
        }

        public string[] ReadRegistryValueData(RegistryHive registryHive, RegistryKey registryKey, string subKey, string valueName)
        {
            string[] instanceNames = new string[0];

            int key = GetRegistryHiveKey(registryHive);
            UIntPtr registryKeyUIntPtr = GetRegistryKeyUIntPtr(registryKey);

            IntPtr hResult;

            int res = RegOpenKeyEx(registryKeyUIntPtr, subKey, 0, KEY_QUERY_VALUE | key, out hResult);

            if (res == 0)
            {
                uint type;
                uint dataLen = 0;

                RegQueryValueEx(hResult, valueName, 0, out type, IntPtr.Zero, ref dataLen);

                byte[] databuff = new byte[dataLen];
                byte[] temp = new byte[dataLen];

                List<String> values = new List<string>();

                GCHandle handle = GCHandle.Alloc(databuff, GCHandleType.Pinned);
                try
                {
                    RegQueryValueEx(hResult, valueName, 0, out type, handle.AddrOfPinnedObject(), ref dataLen);
                }
                finally
                {
                    handle.Free();
                }

                int i = 0;
                int j = 0;

                while (i < databuff.Length)
                {
                    if (databuff[i] == '\0')
                    {
                        j = 0;
                        string str = Encoding.Default.GetString(temp).Trim('\0');

                        if (!string.IsNullOrEmpty(str))
                        {
                            values.Add(str);
                        }

                        temp = new byte[dataLen];
                    }
                    else
                    {
                        temp[j++] = databuff[i];
                    }

                    ++i;
                }

                instanceNames = new string[values.Count];
                values.CopyTo(instanceNames);
            }

            return instanceNames;
        }

        public static IList<string> GetLocalSqlServerInstanceNames()
        {
            RegistryValueDataReader registryValueDataReader = new RegistryValueDataReader();

            string[] instances64Bit = registryValueDataReader.ReadRegistryValueData(RegistryHive.Wow64,
                                                                                    Registry.LocalMachine,
                                                                                    @"SOFTWARE\Microsoft\Microsoft SQL Server",
                                                                                    "InstalledInstances");

            string[] instances32Bit = registryValueDataReader.ReadRegistryValueData(RegistryHive.Wow6432,
                                                                                    Registry.LocalMachine,
                                                                                    @"SOFTWARE\Microsoft\Microsoft SQL Server",
                                                                                    "InstalledInstances");

            IList<string> localInstanceNames = new List<string>(instances64Bit);

            localInstanceNames = localInstanceNames.Union(instances32Bit).ToList();

            return localInstanceNames;
        }
    }
}
