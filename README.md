* If you are using Windows 11, please download the latest version from the [Microsoft Store](https://www.microsoft.com/en-us/p/smarttaskbar/9pjm69mps6t9?activetab=pivot%3aoverviewtab).

  如果你使用的是 Windows 11 请到微软[应用商店](https://www.microsoft.com/zh-cn/p/smarttaskbar/9pjm69mps6t9?activetab=pivot%3aoverviewtab#)下载最新版本。

SmartTaskbar  <img src="https://github.com/ChanpleCai/SmartTaskbar/blob/main/logo/logo.png" width="24">
=====
[![Version](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.1.9/SmartTaskbar_Setup.exe)
[![Latest Release](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/latest/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.1.9/SmartTaskbar_Setup.exe)
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
  
* Some applications are not compatible with Auto Mode and Adaptive Mode, for example:
  
    * If you use the Dell Display Manager, the taskbar will switch between the show and hide frequently.
      
    * Some applications, such as Steam, cannot be maximized properly(After the taskbar is hidden, it will leave a blank space).

Build
-----
* Visual Studio 2022.

Notice
------
<a name="footnote"> The status of the taskbar does not change when the mouse is over the taskbar.</a>  
