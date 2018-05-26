#include "stdafx.h"
#include "TaskbarSwitcher.h"

extern "C" {

    inline __declspec(dllexport) BOOL IsTaskbarAutoHide(PAPPBARDATA msgData) {
        return (BOOL)SHAppBarMessage(ABM_GETSTATE, msgData);
    }

    inline __declspec(dllexport) void ShowTaskbar(PAPPBARDATA msgData) {
        msgData->lParam = ABS_ALWAYSONTOP;
        SHAppBarMessage(ABM_SETSTATE, msgData);
    }

    inline __declspec(dllexport) void HideTaskbar(PAPPBARDATA msgData) {
        msgData->lParam = ABS_AUTOHIDE;
        SHAppBarMessage(ABM_SETSTATE, msgData);
    }

    inline __declspec(dllexport) BOOL IsCursorOverTaskbar(PPOINT cursor, PAPPBARDATA msgData) {
        SHAppBarMessage(ABM_GETTASKBARPOS, msgData);
        GetCursorPos(cursor);
        switch (msgData->uEdge)
        {
        case ABE_BOTTOM:
            if (cursor->y >= msgData->rc.top)
                return TRUE;
            return FALSE;
        case ABE_LEFT:
            if (cursor->x <= msgData->rc.right)
                return TRUE;
            return FALSE;
        case ABE_TOP:
            if (cursor->y <= msgData->rc.bottom)
                return TRUE;
            return FALSE;
        default:
            if (cursor->x >= msgData->rc.left)
                return TRUE;
            return FALSE;
        }
    }
    inline __declspec(dllexport) BOOL IsWindowNotMax(HWND hwnd, PWINDOWPLACEMENT placement) {
        GetWindowPlacement(hwnd, placement);
        if (placement->showCmd != SW_MAXIMIZE)
            return TRUE;
        return FALSE;
    }

    inline __declspec(dllexport) BOOL SetuwpPID(PDWORD uwpPID) {
        //https://stackoverflow.com/questions/865152/how-can-i-get-a-process-handle-by-its-name-in-c
        PROCESSENTRY32 entry;
        entry.dwSize = sizeof(PROCESSENTRY32);
        HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);
        if (Process32First(snapshot, &entry) == TRUE)
            while (Process32Next(snapshot, &entry) == TRUE)
                if (_tcsicmp(entry.szExeFile, TEXT("ApplicationFrameHost.exe")) == 0) {
                    *uwpPID = entry.th32ProcessID;
                    CloseHandle(snapshot);
                    return TRUE;
                }
        CloseHandle(snapshot);
        return FALSE;
    }

    inline __declspec(dllexport) BOOL IsWindowMax(HWND maxWindow, PWINDOWPLACEMENT placement) {
        Sleep(500);
        if (IsWindowVisible(maxWindow) == FALSE)
            return FALSE;
        GetWindowPlacement(maxWindow, placement);
        if (placement->showCmd == SW_MAXIMIZE)
            return TRUE;
        return FALSE;
    }

}




