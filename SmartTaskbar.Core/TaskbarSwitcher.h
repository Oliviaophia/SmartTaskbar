#pragma once
extern "C" {

    __declspec(dllexport) BOOL IsTaskbarAutoHide(PAPPBARDATA msgData);

    __declspec(dllexport) void ShowTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) void HideTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) BOOL IsCursorOverTaskbar(PPOINT cursor, PAPPBARDATA msgData);

    __declspec(dllexport) BOOL IsWindowNotMax(HWND hwnd, PWINDOWPLACEMENT placement);

    __declspec(dllexport) BOOL SetuwpPID(PDWORD uwpPID);

    __declspec(dllexport) BOOL IsWindowMax(HWND maxWindow, PWINDOWPLACEMENT placement);

}

