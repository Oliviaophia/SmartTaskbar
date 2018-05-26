#pragma once
extern "C" {

    __declspec(dllexport) void SwitchTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) bool IsTaskbarAutoHide(PAPPBARDATA msgData);

    __declspec(dllexport) void ShowTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) void HideTaskbar(PAPPBARDATA msgData);

    //__declspec(dllexport) bool IsCursorOverTaskbar(PPOINT cursor, PAPPBARDATA msgData);

    //__declspec(dllexport) bool NonMaximized(HWND hwnd, PWINDOWPLACEMENT placement);

    //__declspec(dllexport) bool NonMaximizedWin10(HWND hwnd, PDWORD windowPID, PDWORD uwpPID, PWINDOWPLACEMENT placement);

    //__declspec(dllexport) void WhileMaximized(HWND maxWindow, PWINDOWPLACEMENT placement);

    //__declspec(dllexport) bool SetuwpPID(PDWORD uwpPID);

}

