SmartTaskbar
===
[![GitHub version](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar.svg)](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar)
[![Github All Releases](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases)
* SmartTaskbar is a small application which can automatically switch the display state of the Windows Taskbar.

  SmartTaskbar是一个能自动切换Windows任务栏显示状态的小程序
  
![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Hide_Show.gif)

Features 功能
-----

* Double-click the tray icon to switch the status of the taskbar between AlwaysOnTop Mode or Auto-Hide Mode.

  双击系统托盘可以快速切换任务栏的显示状态


#### Auto Mode 自动模式

* In the Auto Mode, SmartTaskbar will set the Taskbar to Auto-Hide Mode when a maximized window exists (except UWP window).

  当有窗体最大化时（UWP除外），它会使任务栏自动隐藏
* Without maximized window (except UWP window)， the Taskbar will display automatically（AlwaysOnTop Mode）.

  当没有窗体最大化时（UWP除外），它会自动使任务栏显示
* The Taskbar won't change the display state as the mouse hovering over it.

  当有鼠标悬停在任务栏上方时，不改变任务栏状态

Installation 安装
-----
* Download `SmartTaskbar_Setup.exe` from [releases](https://github.com/ChanpleCai/SmartTaskbar/releases). 

  从[releases](https://github.com/ChanpleCai/SmartTaskbar/releases)下载安装程序
  
* If you do not like the GUI version of SmartTaskbar, you can download version v1.0.3 from [HERE](https://github.com/ChanpleCai/SmartTaskbar/releases/tag/v1.0.3).

  如果你不需要带GUI版本的Smartaskbar，可以在[这里](https://github.com/ChanpleCai/SmartTaskbar/releases/tag/v1.0.3)下载v1.0.3版本
  
* The program will start running in the background after the installtion.

  安装完成后，程序会自动在后台运行
  
* This application takes few resources so that you can forget its existence.

  该程序只会占用极少的系统资源，你可以忘记它的存在

Known Issues 已知问题
----
* Windows 10 version cannot autohide or display automatically based on UWP window state.

  Windows 10版不能根据UWP窗体状态自动隐藏或显示
* When using multiple monitors, this application may not work correctly.

  在使用多显示屏时不能正常工作

Build 生成
-----
* Install Visual Studio 2017.

  安装Visual Studio 2017 
  
Demo 演示
----
![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Open_Close2.gif)

![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Open_Close.gif)

![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Maximize_Button.gif)

![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Shortcut_Key.gif)

![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/block_UWP.gif)
