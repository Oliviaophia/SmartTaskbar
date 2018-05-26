#include "stdafx.h"
#include "TaskbarSwitcher.h"

extern "C" {

    __declspec(dllexport) void SwitchTaskbar(PAPPBARDATA msgData) {
        if (IsTaskbarAutoHide(msgData)) {
            ShowTaskbar(msgData);
        }
        else {
            HideTaskbar(msgData);
        }
    }

    __declspec(dllexport) bool IsTaskbarAutoHide(PAPPBARDATA msgData) {
        return SHAppBarMessage(ABM_GETSTATE, msgData);
    }

    __declspec(dllexport) void ShowTaskbar(PAPPBARDATA msgData) {
        (*msgData).lParam = ABS_ALWAYSONTOP;
        SHAppBarMessage(ABM_SETSTATE, msgData);
    }

    __declspec(dllexport) void HideTaskbar(PAPPBARDATA msgData) {
        (*msgData).lParam = ABS_AUTOHIDE;
        SHAppBarMessage(ABM_SETSTATE, msgData);
    }

    //__declspec(dllexport) bool IsCursorOverTaskbar(PPOINT cursor, PAPPBARDATA msgData) {
    //    SHAppBarMessage(ABM_GETTASKBARPOS, msgData);
    //    GetCursorPos(cursor);
    //    switch (msgData->uEdge)
    //    {
    //    case ABE_BOTTOM:
    //        if (cursor->y >= msgData->rc.top)
    //            return true;
    //        return false;
    //    case ABE_LEFT:
    //        if (cursor->x <= msgData->rc.right)
    //            return true;
    //        return false;
    //    case ABE_TOP:
    //        if (cursor->y <= msgData->rc.bottom)
    //            return true;
    //        return false;
    //    default:
    //        if (cursor->x >= msgData->rc.left)
    //            return true;
    //        return false;
    //    }
    //}

    //__declspec(dllexport) bool NonMaximized(HWND hwnd, PWINDOWPLACEMENT placement) {
    //    if (IsWindowVisible(hwnd) == FALSE)
    //        return true;
    //    GetWindowPlacement(hwnd, placement);
    //    if (placement->showCmd != SW_MAXIMIZE)
    //        return true;
    //    return false;
    //}

    //__declspec(dllexport) bool NonMaximizedWin10(HWND hwnd, PDWORD windowPID, PDWORD uwpPID, PWINDOWPLACEMENT placement) {
    //    if (IsWindowVisible(hwnd) == FALSE)
    //        return true;
    //    GetWindowPlacement(hwnd, placement);
    //    if (placement->showCmd != SW_MAXIMIZE)
    //        return true;
    //    GetWindowThreadProcessId(hwnd, windowPID);
    //    if (*uwpPID == *windowPID)
    //        return true;
    //    return false;
    //}

    //__declspec(dllexport) void WhileMaximized(HWND maxWindow, PWINDOWPLACEMENT placement) {
    //    do {
    //        Sleep(500);
    //        if (IsWindowVisible(maxWindow) == FALSE)
    //            break;
    //        GetWindowPlacement(maxWindow, placement);
    //    } while ((*placement).showCmd == SW_MAXIMIZE);
    //}

    //__declspec(dllexport) bool SetuwpPID(PDWORD uwpPID) {
    //    //https://stackoverflow.com/questions/865152/how-can-i-get-a-process-handle-by-its-name-in-c
    //    PROCESSENTRY32 entry;
    //    entry.dwSize = sizeof(PROCESSENTRY32);
    //    HANDLE snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);
    //    if (Process32First(snapshot, &entry) == TRUE)
    //        while (Process32Next(snapshot, &entry) == TRUE)
    //            if (_tcsicmp(entry.szExeFile, TEXT("ApplicationFrameHost.exe")) == 0) {
    //                *uwpPID = entry.th32ProcessID;
    //                CloseHandle(snapshot);
    //                return true;
    //            }
    //    CloseHandle(snapshot);
    //    return false;
    //}

}




