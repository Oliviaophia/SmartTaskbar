SmartTaskbar  [![Logo](https://github.com/ChanpleCai/SmartTaskbar/blob/master/logo/logo_blue_24x24.png)](http://www.softpedia.com/get/Tweak/System-Tweak/SmartTaskbar.shtml)
=====
[![GitHub version](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.1.2/SmartTaskbar_Setup.exe)
[![Github All Releases](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases)
[![License](http://img.shields.io/:license-MIT-blue.svg?style=flat)](LICENSE)

* SmartTaskbar is a small application which can automatically switch the display state of the Windows Taskbar.

  SmartTaskbar是一个能自动切换Windows任务栏显示状态的小程序
  
![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Context_Menu.gif)

Features 功能
-----

* Double-click the tray icon to switch the status of the taskbar between AlwaysOnTop Mode or Auto-Hide Mode.

  双击系统托盘可以快速切换任务栏的显示状态
  
![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Double-click.gif)

#### Auto Mode 自动模式

* In the Auto Mode, SmartTaskbar will set the Taskbar to Auto-Hide Mode when a maximized window exists (except UWP window).

  当有窗体最大化时（UWP除外），它会使任务栏自动隐藏
* Without maximized window (except UWP window), the Taskbar will display automatically (AlwaysOnTop Mode).

  当没有窗体最大化时（UWP除外），它会自动使任务栏显示
* The Taskbar won't change the display state as the mouse hovering over it.

  当有鼠标悬停在任务栏上方时，不改变任务栏状态

![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Hide_Show.gif)

* [Click for more demo 点击查看更多演示](https://github.com/ChanpleCai/SmartTaskbar/tree/master/demo)
  
Installation 安装
-----
* Download `SmartTaskbar_Setup.exe` from [releases](https://github.com/ChanpleCai/SmartTaskbar/releases) or [Softpedia](http://www.softpedia.com/get/Tweak/System-Tweak/SmartTaskbar.shtml)： 

  从[releases](https://github.com/ChanpleCai/SmartTaskbar/releases)或[Softpedia](http://www.softpedia.com/get/Tweak/System-Tweak/SmartTaskbar.shtml)下载安装程序：
  
  [![Download Now](https://github.com/ChanpleCai/SmartTaskbar/blob/master/img/Download_Softpedia.png)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.1.2/SmartTaskbar_Setup.exe) 
  
    * If the installation fails, please re-install after installing the [.NET Framework 4.7.1](https://www.microsoft.com/net/download/dotnet-framework-runtime/net471?utm_source=getdotnet&utm_medium=referral).

      如果安装失败，请在安装完[.NET Framework 4.7.1](https://www.microsoft.com/net/download/dotnet-framework-runtime/net471?utm_source=getdotnet&utm_medium=referral)框架后重新安装一次

    * If you do not like the GUI version of SmartTaskbar, you can download version v1.0.3 from [HERE](https://github.com/ChanpleCai/SmartTaskbar/releases/tag/v1.0.3).

      如果您不需要带GUI版本的SmartTaskbar，可以在[这里](https://github.com/ChanpleCai/SmartTaskbar/releases/tag/v1.0.3)下载v1.0.3版本

* The program will start running in the background after the installtion.

  安装完成后，程序会自动在后台运行
  
* This application takes few resources so that you can forget its existence.

  该程序只会占用极少的系统资源，你可以忘记它的存在

Known Issues 已知问题
----
* The SmartTaskbar(Auto Mode) cannot autohide or display automatically based on UWP window state.

  SmartTaskbar(Auto Mode)不能根据UWP窗体状态自动隐藏或显示
  
* When using multiple monitors, Auto Mode may not work correctly.

  在使用多显示屏时自动模式不能正确工作
  
* Some applications are not compatible with Auto Mode, for example:

  有些应用不能和自动模式兼容，例如：
  
    * If you use the Dell Display Manager, the taskbar will switch between the AlwaysOnTop Mode and Auto-Hide Mode frequently.
    
      如果你同时安装了Dell Display Manager，任务栏会频繁地在显示和隐藏模式间来回切换
      
    * Some applications, such as Steam, cannot be maximized properly(After the taskbar is hidden, it will leave a blank space).
    
      有些应用程序如Steam，不能正常的最大化(任务栏隐藏后会留下一道空隙)
      
* Under Windows 10, sometimes the taskbar(Auto-Hide Mode) can't be automatically hidden, the taskbar will blocks the content behind it(This is probably a bug in Windows 10).

  在Windows 10下，有时自动隐藏模式的任务栏，会掩盖下方的内容，而不能真正的自动隐藏(这很可能是Windows 10系统的一个bug)

Build 生成
-----
* Install Visual Studio 2017.

  安装 Visual Studio 2017 
