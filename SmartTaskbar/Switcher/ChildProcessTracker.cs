using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static SmartTaskbar.SafeNativeMethods;

namespace SmartTaskbar
{
    //https://stackoverflow.com/questions/3342941/kill-child-process-when-parent-process-is-killed
    class ChildProcessTracker
    {
        public void AddProcess(Process process)
        {
            if (s_jobHandle != IntPtr.Zero)
            {
                bool success = AssignProcessToJobObject(s_jobHandle, process.Handle);
                if (!success)
                    return;
            }
        }

        public ChildProcessTracker()
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
                        LimitFlags = JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE
                    }
                }, extendedInfoPtr, false);
                SetInformationJobObject(s_jobHandle, ExtendedLimitInformation, extendedInfoPtr, (uint)length);
            }
            finally
            {
                Marshal.FreeHGlobal(extendedInfoPtr);
            }
        }

        private readonly IntPtr s_jobHandle;
    }
}
