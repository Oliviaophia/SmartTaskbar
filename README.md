SmartTaskbar
===
SmartTaskbar是一个能自动切换Windows显示状态的小程序

Features 功能
----
* 当有窗体最大化时，它会自动使任务栏隐藏
* 当没有窗体最大化时，它会自动使任务栏显示
* 当有鼠标悬停在任务栏上方时，不改变任务栏状态

Installation 安装
----
首先从[releases](https://github.com/ChanpleCai/SmartTaskbar/releases)下载对应版本

    32位Windows下载x86版本，64位Windows下载x64版本
    win10以前的系统下载SmartTaskbar_xxx.exe
    win10系统下载SmartTaskbarWin10_xxx.exe

双击程序即可运行

### 设置开机自启

下载完成后，将文件复制到启动文件夹
* 直接拖拽进Startup.lnk快捷方式
* 或win+R运行 `shell:startup` 打开启动文件夹，将程序复制进去

### 结束程序

进入任务管理器详细信息页，找到并选中名为`SmartTaskbar`或`SmartTaskbarWin10`的程序，点击结束任务

卸载
----
直接删除程序文件即可卸载

已知问题
-----
* 不能根据UWP窗体的状态改变任务栏状态
* 在多显示器下不能正常工作

Build 构建
-----
* 安装Visual studio 2017 
