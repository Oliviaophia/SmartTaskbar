SmartTaskbar  ![Logo](https://github.com/ChanpleCai/SmartTaskbar/blob/master/logo/logo_blue_24x24.png)
=====
[![Version](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.1.7/SmartTaskbar_Setup.exe)
[![Latest Release](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/latest/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.1.7/SmartTaskbar_Setup.exe)
[![All Releases](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases)
[![License](http://img.shields.io/:license-MIT-blue.svg?style=flat)](LICENSE)

* SmartTaskbar is a lightweight utility which can automatically switch the display state of the Windows Taskbar.
  
![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Context_Menu.gif)

Features
-----

#### Auto Mode

* In the Auto Mode, SmartTaskbar will set the Taskbar to Auto-Hide(Hide) Mode when a maximized window exists .

  当有窗体最大化时，它会使任务栏自动隐藏
  
* Without maximized window, the Taskbar will display(Show) automatically.

  当没有窗体最大化时，它会自动使任务栏显示

![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Hide_Show.gif)

* Double-click the tray icon to switch the display status of the taskbar between Show or Auto-Hide.
  
    双击系统托盘图标切换任务栏显示和隐藏状态
  
![img](https://github.com/ChanpleCai/SmartTaskbar/blob/master/demo/Double-click.gif)

* [Click for more demo 点击查看更多演示](https://github.com/ChanpleCai/SmartTaskbar/tree/master/demo)
  
Installation
-----
* Download `SmartTaskbar_Setup.exe` from [releases](https://github.com/ChanpleCai/SmartTaskbar/releases):

  从[releases](https://github.com/ChanpleCai/SmartTaskbar/releases)下载：
  
  [![Download Now](https://github.com/ChanpleCai/SmartTaskbar/blob/master/img/Download_Softpedia.png)](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.1.7/SmartTaskbar_Setup.exe) 

* It will start running in the background after the installtion.

  安装完成后，程序会自动在后台运行

Known Issues
----
  
* SmartTaskbar is designed for single-monitor users, so Auto-Mode does not work correctly when using multiple monitors.

  SmartTaskbar是为单显示器用户设计的，在使用多显示屏时自动模式不能正确工作
  
* Some applications are not compatible with Auto Mode, for example:

  有些应用不能和自动模式兼容，例如：
  
    * If you use the Dell Display Manager, the taskbar will switch between the Show Mode and Auto-Hide(Hide) Mode frequently.
    
      如果你同时在使用Dell Display Manager，任务栏会频繁地在显示和隐藏模式间来回切换
      
    * Some applications, such as Steam, cannot be maximized properly(After the taskbar is hidden, it will leave a blank space).
    
      有些应用程序如Steam，不能正常的最大化(任务栏隐藏后会留下一道空隙)

Build
-----
* Install Visual Studio 2017.
