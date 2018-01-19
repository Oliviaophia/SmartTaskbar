SmartTaskbar
===
[![GitHub version](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar.svg)](https://badge.fury.io/gh/ChanpleCai%2FSmartTaskbar)
[![Github All Releases](https://img.shields.io/github/downloads/ChanpleCai/SmartTaskbar/total.svg)](https://github.com/ChanpleCai/SmartTaskbar/releases)
* SmartTaskbar is a small program which can automatically switch the display state of the Windows Taskbar.

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
* Download `SmartTaskbar_xxx.exe` from [releases](https://github.com/ChanpleCai/SmartTaskbar/releases). Select the version that suits your operating system:

  首先从[releases](https://github.com/ChanpleCai/SmartTaskbar/releases)下载对应版本：

    * x86 version for the 32-bit operating system, x64 version for the 64-bit operating system.
      
      32位Windows请下载x86版本，64位Windows请下载x64版本
      
    * `SmartTaskbar_xxx.exe` for win8.1 or earlier system.
      
      win8.1及以前的系统请下载`SmartTaskbar_xxx.exe`
      
    * `SmartTaskbarWin10_xxx.exe` for win10.
      
      win10系统请下载`SmartTaskbarWin10_xxx.exe`
      
    * The Windows Vista and earlier system are not supported.
      
      该程序不支持Windows Vista及以前的系统
* Double-click the `exe` file to run the program, and it will automatically run in the background.

  双击`exe`运行程序，它将自动在后台运行
  
* This program takes few resources so that you can forget its existence after the installation.

  该程序只会占用极少的系统资源，安装完成后便可以忘记它的存在
  
Autostart 开机自启
-----
Copy the program to the Startup folder, it will autostart after system boots up. The folder is located at `%AppData%\Microsoft\Windows\Start Menu\Programs\Startup`


  复制程序到启动文件夹，即可开机启动。文件夹位于`%AppData%\Microsoft\Windows\Start Menu\Programs\Startup`
* You can drag the program file directly into the [Startup.lnk](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.0.0/Startup.lnk).
  
  你可以直接拖拽程序到[Startup.lnk](https://github.com/ChanpleCai/SmartTaskbar/releases/download/v1.0.0/Startup.lnk)
* Or you can directly access this folder, by open Run( <kbd>Win</kbd> + <kbd>R</kbd> ), type `shell:startup` and hit Enter.
  
  或打开运行（<kbd>Win</kbd> + <kbd>R</kbd>），输入： `shell:startup` ，回车，直接打开启动文件夹

Close 关闭
----
Using Task Manage to find and select `SmartTaskbar_xxx.exe`, then click on the End Task button to stop it.

  使用任务管理器找到并选中`SmartTaskbar_xxx.exe`，点击结束任务关闭

Uninstall 卸载
----
Delete the program file to uninstall.

  删除程序文件即可卸载

Known Issues 已知问题
----
* Windows 10 version cannot autohide or display automatically based on UWP window state.

  Windows 10版不能根据UWP窗体状态自动隐藏或显示
* The program may not work properly when using multiple monitors.

  在使用多显示屏时不能正常工作
* If the program can't start and show the error message that `VCRUNTIME140.dll` is missing. Please [download and install the Microsoft Visual C++ Redistributable  Packages](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads)

  如果提示缺失`VCRUNTIME140.dll`无法运行，请[下载并安装VC++运行库](https://support.microsoft.com/zh-cn/help/2977003/the-latest-supported-visual-c-downloads)

Build 构建
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
