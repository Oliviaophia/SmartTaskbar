using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace SmartTaskbar
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {

        #region SHAppBarMessage

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public static APPBARDATA New()
            {
                return new APPBARDATA
                {
                    cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA))
                };
            }

            /// DWORD->unsigned int
            public uint cbSize;

            /// HWND->HWND__*
            public IntPtr hWnd;

            /// UINT->unsigned int
            public uint uCallbackMessage;

            /// UINT->unsigned int
            public uint uEdge;

            /// RECT->TagRECT
            public TagRECT rc;

            /// LPARAM->LONG_PTR->int
            public int lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TagRECT
        {

            /// LONG->int
            public int left;

            /// LONG->int
            public int top;

            /// LONG->int
            public int right;

            /// LONG->int
            public int bottom;
        }

        /// Return Type: UINT_PTR->unsigned int
        ///dwMessage: DWORD->unsigned int
        ///pData: PAPPBARDATA->_AppBarData*
        [DllImport("shell32.dll", EntryPoint = "SHAppBarMessage", CallingConvention = CallingConvention.StdCall)]
        private static extern uint SHAppBarMessage(uint dwMessage, ref APPBARDATA pData);

        public static void Hide(ref APPBARDATA msgData)
        {
            msgData.lParam = 1;
            SHAppBarMessage(10, ref msgData);
        }

        public static void Show(ref APPBARDATA msgData)
        {
            msgData.lParam = 0;
            SHAppBarMessage(10, ref msgData);
        }

        public static bool IsHide(ref APPBARDATA msgData) => SHAppBarMessage(4, ref msgData) == 1 ? true : false;
        #endregion

        #region JobObject
        private static readonly IntPtr s_jobHandle;

        static SafeNativeMethods()
        {
            s_jobHandle = CreateJobObjectW(IntPtr.Zero, null);

            int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            IntPtr extendedInfoPtr = Marshal.AllocHGlobal(length);
            try
            {
                Marshal.StructureToPtr(new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
                {
                    BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION
                    {
                        LimitFlags = 0x2000
                    }
                }, extendedInfoPtr, false);
                SetInformationJobObject(s_jobHandle, 9, extendedInfoPtr, (uint)length);
            }
            finally
            {
                Marshal.FreeHGlobal(extendedInfoPtr);
            }

        }

        public static void AddProcess(Process process)
        {
            if (s_jobHandle == IntPtr.Zero)
                return;
            AssignProcessToJobObject(s_jobHandle, process.Handle);
        }


        /// Return Type: HANDLE->void*
        ///lpJobAttributes: LPSECURITY_ATTRIBUTES->_SECURITY_ATTRIBUTES*
        ///lpName: LPCWSTR->WCHAR*
        [DllImport("kernel32.dll", EntryPoint = "CreateJobObjectW")]
        private static extern IntPtr CreateJobObjectW(IntPtr lpJobAttributes, [MarshalAs(UnmanagedType.LPWStr)] string lpName);

        /// Return Type: BOOL->int
        ///hJob: HANDLE->void*
        ///JobObjectInformationClass: JOBOBJECTINFOCLASS->_JOBOBJECTINFOCLASS
        ///lpJobObjectInformation: LPVOID->void*
        ///cbJobObjectInformationLength: DWORD->unsigned int
        [DllImport("kernel32.dll", EntryPoint = "SetInformationJobObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetInformationJobObject(IntPtr hJob, int JobObjectInformationClass, IntPtr lpJobObjectInformation, uint cbJobObjectInformationLength);


        /// Return Type: BOOL->int
        ///hJob: HANDLE->void*
        ///hProcess: HANDLE->void*
        [DllImport("kernel32.dll", EntryPoint = "AssignProcessToJobObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AssignProcessToJobObject([In()] IntPtr hJob, [In()] IntPtr hProcess);

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            public Int64 PerProcessUserTimeLimit;
            public Int64 PerJobUserTimeLimit;
            public UInt32 LimitFlags;
            public UIntPtr MinimumWorkingSetSize;
            public UIntPtr MaximumWorkingSetSize;
            public UInt32 ActiveProcessLimit;
            public Int64 Affinity;
            public UInt32 PriorityClass;
            public UInt32 SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IO_COUNTERS
        {
            public UInt64 ReadOperationCount;
            public UInt64 WriteOperationCount;
            public UInt64 OtherOperationCount;
            public UInt64 ReadTransferCount;
            public UInt64 WriteTransferCount;
            public UInt64 OtherTransferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
            public IO_COUNTERS IoInfo;
            public UIntPtr ProcessMemoryLimit;
            public UIntPtr JobMemoryLimit;
            public UIntPtr PeakProcessMemoryUsed;
            public UIntPtr PeakJobMemoryUsed;
        }
        #endregion

        #region TaskbarAnimation

        /// Return Type: BOOL->int
        ///uiAction: UINT->unsigned int
        ///uiParam: UINT->unsigned int
        ///pvParam: PVOID->void*
        ///fWinIni: UINT->unsigned int
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetSystemParameters(uint uiAction, uint uiParam, bool pvParam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetSystemParameters(uint uiAction, uint uiParam, out bool pvParam, uint fWinIni);

        public static bool GetTaskbarAnimation(out bool animation)
        {
            GetSystemParameters(0x1002, 0, out animation, 0);
            return animation;
        }

        public static bool ChangeTaskbarAnimation(ref bool animation)
        {
            animation = !animation;
            SetSystemParameters(0x1003, 0, animation, 0x01 | 0x02);
            return animation;
        }
        #endregion

        #region SendNotifyMessageW

        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        ///Msg: UINT->unsigned int
        ///wParam: WPARAM->UINT_PTR->unsigned int
        ///lParam: LPARAM->LONG_PTR->int
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam);


        public static void SetIconSize(int size)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true))
            {
                key.SetValue("TaskbarSmallIcons", size);
                SendNotifyMessage((IntPtr)0xffff, 0x001a, (UIntPtr)0, "TraySettings");
            }
        }

        #endregion
    }
}
