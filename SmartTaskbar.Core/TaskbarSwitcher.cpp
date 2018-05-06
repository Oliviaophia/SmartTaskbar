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

}






