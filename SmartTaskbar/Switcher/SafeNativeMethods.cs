using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace SmartTaskbar
{
    [SuppressUnmanagedCodeSecurity]
    static class SafeNativeMethods
    {
        static SafeNativeMethods()
        {
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
            //Transparent taskbar is only valid in win10
            if (Environment.OSVersion.Version.Major.ToString() == "10")
            {
                var accent = new AccentPolicy()
                {
                    AccentState = 2,
                    AccentFlags = 2,
                    GradientColor = 0
                };

                var accentPtr = Marshal.AllocHGlobal(Marshal.SizeOf(accent));
                Marshal.StructureToPtr(accent, accentPtr, false);

                data = new WindowCompositionAttributeData()
                {
                    Attribute = 19,
                    SizeOfData = Marshal.SizeOf(accent),
                    Data = accentPtr
                };

                taskbar = FindWindowW("Shell_TrayWnd", null);
            }
        }


        #region Taskbar Display State

        private static APPBARDATA msgData = new APPBARDATA { cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA)) }; 

        [StructLayout(LayoutKind.Sequential)]
        private struct APPBARDATA
        {

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
        private struct TagRECT
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
        /// <summary>
        /// Set Auto-Hide Mode
        /// </summary>
        public static void Hide()
        {
            msgData.lParam = 1;
            SHAppBarMessage(10, ref msgData);
        }
        /// <summary>
        /// Set AlwaysOnTop Mode
        /// </summary>
        public static void Show()
        {
            msgData.lParam = 0;
            SHAppBarMessage(10, ref msgData);
        }
        /// <summary>
        /// Indicate if the Taskbar is Auto-Hide
        /// </summary>
        /// <returns>Return true when Auto-Hide</returns>
        public static bool IsHide() => SHAppBarMessage(4, ref msgData) == 1 ? true : false;
        #endregion

        #region Job Container

        private static readonly IntPtr s_jobHandle = CreateJobObjectW(IntPtr.Zero, null);

        /// <summary>
        /// Add process to current Job
        /// </summary>
        /// <param name="handle">Process handle</param>
        public static void AddProcess(IntPtr handle)
        {
            if (s_jobHandle == IntPtr.Zero)
                return;
            AssignProcessToJobObject(s_jobHandle, handle);
        }


        /// Return Type: HANDLE->IntPtr
        ///lpJobAttributes: LPSECURITY_ATTRIBUTES->IntPtr
        ///lpName: LPCWSTR->string
        [DllImport("kernel32.dll", EntryPoint = "CreateJobObjectW")]
        private static extern IntPtr CreateJobObjectW(IntPtr lpJobAttributes, [MarshalAs(UnmanagedType.LPWStr)] string lpName);

        /// Return Type: BOOL->bool
        ///hJob: HANDLE->IntPtr
        ///JobObjectInformationClass: JOBOBJECTINFOCLASS->int
        ///lpJobObjectInformation: LPVOID->IntPtr
        ///cbJobObjectInformationLength: DWORD->uint
        [DllImport("kernel32.dll", EntryPoint = "SetInformationJobObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetInformationJobObject(IntPtr hJob, int JobObjectInformationClass, IntPtr lpJobObjectInformation, uint cbJobObjectInformationLength);


        /// Return Type: BOOL->bool
        ///hJob: HANDLE->IntPtr
        ///hProcess: HANDLE->IntPtr
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

        #region Taskbar Animation

        /// Return Type: BOOL->bool
        ///uiAction: UINT->uint
        ///uiParam: UINT->uint
        ///pvParam: PVOID->bool
        ///fWinIni: UINT->uint
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetSystemParameters(uint uiAction, uint uiParam, bool pvParam, uint fWinIni);

        /// Return Type: BOOL->bool
        ///uiAction: UINT->uint
        ///uiParam: UINT->uint
        ///pvParam: PVOID->out bool
        ///fWinIni: UINT->uint
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetSystemParameters(uint uiAction, uint uiParam, out bool pvParam, uint fWinIni);

        private static bool animation;
        /// <summary>
        /// Get the taskbar animation state
        /// </summary>
        /// <returns>Taskbar animation state</returns>
        public static bool GetTaskbarAnimation()
        {
            GetSystemParameters(0x1002, 0, out animation, 0);
            return animation;
        }
        /// <summary>
        /// Change the taskbar animation state
        /// </summary>
        /// <returns>Taskbar animation state</returns>
        public static bool ChangeTaskbarAnimation()
        {
            animation = !animation;
            SetSystemParameters(0x1003, 0, animation, 0x01 | 0x02);
            return animation;
        }
        #endregion

        #region Taskbar Buttons Size

        /// Return Type: BOOL->bool
        ///hWnd: HWND->IntPtr
        ///Msg: UINT->uint
        ///wParam: WPARAM->UINT_PTR->UIntPtr
        ///lParam: LPARAM->LONG_PTR->string
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam);

        private static readonly RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
        /// <summary>
        /// Modify the Taskbar buttons size
        /// </summary>
        /// <param name="size">1 = small，0 = big</param>
        public static void SetIconSize(int size)
        {
            key.SetValue("TaskbarSmallIcons", size);
            SendNotifyMessage((IntPtr)0xffff, 0x001a, (UIntPtr)0, "TraySettings");
        }

        #endregion

        #region Transparent Taskbar

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [StructLayout(LayoutKind.Sequential)]
        private struct AccentPolicy
        {
            public int AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WindowCompositionAttributeData
        {
            public int Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        /// Return Type: HWND->IntPtr
        ///lpClassName: LPCWSTR->string
        ///lpWindowName: LPCWSTR->string
        [DllImport("user32.dll", EntryPoint = "FindWindowW")]
        private static extern IntPtr FindWindowW([In()] [MarshalAs(UnmanagedType.LPWStr)] string lpClassName, [In()] [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName);

        private static IntPtr taskbar;
        private static WindowCompositionAttributeData data;
        /// <summary>
        ///  Make Taskbar Transparent
        /// </summary>
        public static void Transparent() => SetWindowCompositionAttribute(taskbar, ref data);
        /// <summary>
        /// Update Taskbar Handle
        /// </summary>
        public static void UpdataTaskbarHandle() => taskbar = FindWindowW("Shell_TrayWnd", null);

        #endregion

    }
}
