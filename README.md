SmartTaskbar
===
[![GitHub version](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar.svg)](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar)
[![Github All Releases](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases)
* SmartTaskbar is a small application which can automatically switch the display state of the Windows Taskbar.

  SmartTaskbar是一个能自动切换Windows任务栏显示状态的小程序
  
![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Hide_Show.gif)

Features 功能
-----
* It will set the Taskbar to Auto-Hide mode when a maximized window exists (except UWP window).

  当有窗体最大化时（UWP除外），它会使任务栏自动隐藏
* Without maximized window (except UWP window)， the Taskbar will display automatically.

  当没有窗体最大化时（UWP除外），它会自动使任务栏显示
* The Taskbar won't change the display state as the mouse hovering over it.

  当有鼠标悬停在任务栏上方时，不改变任务栏状态

Installation 安装
-----
* Download `SmartTaskbar_Setup.exe` from [releases](https://github.com/ChanpleCai/SmartTaskbar/releases). 

  从[releases](https://github.com/ChanpleCai/SmartTaskbar/releases)下载安装程序
  
* This application takes few resources so that you can forget its existence after the installation.

  该程序只会占用极少的系统资源，安装完成后便可以忘记它的存在

Close 关闭
----
Using Task Manage to find and select `SmartTaskbar_xxx.exe`, then click on the End Task button to stop it.

  使用任务管理器找到并选中`SmartTaskbar_xxx.exe`，点击结束任务关闭

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
