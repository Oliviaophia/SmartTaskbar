#pragma once

extern "C" {

    __declspec(dllexport) void SwitchTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) bool IsTaskbarAutoHide(PAPPBARDATA msgData);

    __declspec(dllexport) void ShowTaskbar(PAPPBARDATA msgData);

    __declspec(dllexport) void HideTaskbar(PAPPBARDATA msgData);

    /*__declspec(dllexport) void DefaultAutoMode(PAPPBARDATA msgData, PWINDOWPLACEMENT placement, HWND maxWindow, );*/


}
