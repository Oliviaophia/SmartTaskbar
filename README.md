SmartTaskbar
===
* SmartTaskbar is a small program which can automatically switch the display state of the Windows Taskbar 

  SmartTaskbar是一个能自动切换Windows任务栏显示状态的小程序

Features 功能
-----
* It will set the Taskbar to Auto-Hide mode when there is a maximized window exists (except UWP window)

  当有窗体最大化时（UWP除外），它会使任务栏自动隐藏
* Without maximized window (except UWP window)， the Taskbar will display automatically 

  当没有窗体最大化时（UWP除外），它会自动使任务栏显示
* It won't changed the state of the Taskbar with the mouse hovered over it

  当有鼠标悬停在任务栏上方时，不改变任务栏状态

Installation 安装
-----
* To begin with, you should select the version that correspond your operation system:

  首先从[releases](https://github.com/ChanpleCai/SmartTaskbar/releases)下载对应版本：

    * Choose the 32-bit version or the 64-bit version depend on your X32 or X64 version of Windows
      
      32位Windows请下载x86版本，64位Windows请下载x64版本
      
    * Please download the `martTaskbar_xxx.exe` if your operation system is win8.1 or earlier
      
      win8.1及以前的系统请下载`SmartTaskbar_xxx.exe`
      
    * Please download the `SmartTaskbarWin10_xxx.exe` if your operation system is win10
      
      win10系统请下载`SmartTaskbarWin10_xxx.exe`
      
    * The program does't support the Windows Vista and earlier system
      
      该程序不支持Windows Vista及以下系统
    
* Double-click the download file and the program will auto run

  双击程序即可运行

Autostart 开机自启
-----
Copy the program to the startup folder, it will autostart after system boots up

  复制程序到启动文件夹，即可开机启动
* Drag the file directly into the Startup.lnk shortcut
  
  直接拖拽进Startup.lnk快捷方式
* Or you can open the start up folder by use the `shell:startup` command in 'Win+R' form,then copy the file to the folder

  或win+R运行 `shell:startup` 打开启动文件夹，将程序复制进去

Close 关闭：
----
Using Task Manage to select "SmartTaskbar", then click on the End Task button

  使用任务管理器找到并选中“SmartTaskbar”，点击结束任务

Uninstall 卸载：
----
Delete the program file to uninstall

  删除程序文件即可卸载

Known Issues 已知问题：
----
* Windows 10 version cannot autohide or display automatically based on UWP window state

  Windows 10版不能根据UWP窗体状态自动隐藏或显示
* The program may not work properly when using multiple monitors

  在使用多显示屏时不能正常工作
* If the program can't start because `VCRUNTIME140.dll` is missing. Please [download the Microsoft Visual C++ Redistributable  Packages](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads)

  如果提示缺失`VCRUNTIME140.dll`无法运行，请[下载对应版本的VC++运行库](https://support.microsoft.com/zh-cn/help/2977003/the-latest-supported-visual-c-downloads)

Build 构建
-----
* Install Visual Studio 2017

  安装Visual Studio 2017 
