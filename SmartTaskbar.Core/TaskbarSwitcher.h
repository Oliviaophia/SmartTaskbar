#pragma once
extern "C" {

    __declspec(dllexport) BOOL IsTaskbarAutoHide(PAPPBARDATA msgData);

    __declspec(dllexport) void ShowTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) void HideTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) BOOL IsCursorOverTaskbar(PPOINT cursor, PAPPBARDATA msgData);

    __declspec(dllexport) BOOL CallBack(HWND hwnd, HWND* maxWindow, PWINDOWPLACEMENT placement);

    __declspec(dllexport) BOOL CallBackWin10(HWND hwnd, PDWORD windowPID, PDWORD uwpPID, HWND* maxWindow, PWINDOWPLACEMENT placement);

    __declspec(dllexport) BOOL SetuwpPID(PDWORD uwpPID);

    __declspec(dllexport) void WhileMax(HWND maxWindow, PWINDOWPLACEMENT placement);
}

