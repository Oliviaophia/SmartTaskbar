

SmartTaskbar  <img src="https://github.com/ChanpleCai/SmartTaskbar/blob/main/logo/logo.png" width="24">
=====
[![Version](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.4.3/SmartTaskbar_Setup.exe)
[![Latest Release](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/latest/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.4.3/SmartTaskbar_Setup.exe)
[![All Releases](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases)
[![License](http://img.shields.io/:license-MIT-blue.svg?style=flat)](LICENSE)
[![996.icu](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu)
[![LICENSE](https://img.shields.io/badge/license-Anti%20996-blue.svg)](https://github.com/996icu/996.ICU/blob/master/LICENSE)

* SmartTaskbar is a lightweight utility which can automatically switch the display state of the Windows Taskbar.

Features
-----

#### Auto Mode

* In the Auto Mode, SmartTaskbar will set the Taskbar to hide when When the focused window and the taskbar intersect<sup>[[1]](#footnote)</sup>.
  
* Double-click the tray icon to switch the display status of the taskbar between Show or Auto-Hide.

Known Issues
----  

* Some applications are not compatible with Auto-Hide mode (This problem has nothing to do with SmartTaskbar), for example:
  
    * Some applications use special maximization logic. When the mouse is moved to the taskbar position, the taskbar doesn't pop up. In this case, you can use the shortcut (`WIN` + `T`) to force the taskbar to pop up.

* Starting from [v1.4.3](https://github.com/ChanpleCai/SmartTaskbar/releases), the following behavior only occurs when the hook fails.
>* The Auto Mode is based on Auto-Hide mode, so it does not change the default behavior of window taskbar in Auto-Hide mode. Therefore, you will encounter the following "bugs", but they are not actually:

>    * When you close the start menu or the search panel, the taskbar will be automatically hidden (this is the system's own behavior), and then may be displayed again immediately (this is the work of SmartTaskbar).
   
>    * When there is already a full-screen application, open a window that does not intersect the taskbar, and the taskbar will be automatically hidden when the mouse is moved away (this is the system's own behavior), and then it may be displayed again immediately (this is SmartTaskbar working ), since the currently focused window does not intersect the taskbar.

Build
-----
* Visual Studio 2022.

Notice
------
* <a name="footnote"> The status of the taskbar does not change when the mouse is over the taskbar.</a>  

* The [Microsoft Store](https://www.microsoft.com/en-us/p/smarttaskbar/9pjm69mps6t9?activetab=pivot%3aoverviewtab) version is slightly less functional and stable.
