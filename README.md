<div align="center">
  <img src="HideTB.png" alt="HideTB Logo" width="128" />
  <h1>HideTB</h1>
  <p><strong>Make your Windows taskbar invisible to prevent OLED burn-in without auto-hiding.</strong></p>
   
  [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
  [![Platform](https://img.shields.io/badge/platform-Windows%2010%2F11-blue)](https://github.com/LI-NA/HideTB)
  [![.NET](https://img.shields.io/badge/.NET-10.0-purple)](https://dotnet.microsoft.com/download)

  <p>
    <strong>English</strong> | <a href="README.ko.md">한국어</a>
  </p>
</div>

## 🧐 Why I made this?

I bought a fancy OLED monitor, but I was terrified of **burn-in**.
So I turned on the Windows native "Auto-hide taskbar" feature. And I hated it.

1.  It's buggy.
2.  Whenever I tried to click something at the bottom of a window, the taskbar would pop up and block my click. (Extremely annoying!)

So I wrote this tiny utility. It doesn't "collapse" the taskbar; it just **sets the opacity to 0**. The taskbar is still there, protecting my OLED, but it won't jump scare you when you move your mouse near the bottom.

> **Note:** The previous README was **written by AI** and made this look like some grand enterprise software. It's not. It's just a simple toy project I made for myself, but I'm sharing it in case someone else shares my pain.

## ✨ Features

* **Simple Logic**: It makes the taskbar transparent when you are not using it. That's it.
* **"Intelligent" Visibility**: If the Start Menu or Search window pops up, the taskbar reappears. (Because seeing a floating Start Menu without a taskbar looked ridiculous).
* **Custom Timing**: You can adjust the delay. Honestly, I added this because there was nothing else to configure.
* **"Lightweight"**: The source code is very light. The memory usage? Well... it's .NET. Please don't look at the RAM usage too closely. 🥲

## 📥 Installation

I honestly don't know how to create a fancy installer package yet.

1.  Go to the [**Releases**](https://github.com/LI-NA/HideTB/releases) page.
2.  Download the pre-built zip file.
3.  Unzip and run `HideTB.exe`.

## 🛠️ Tech Stack
* **C# / .NET 10** (Yes, bleeding edge for a tiny tool)
* **WPF**

## 🤝 Contributing

**I am not very good at C#.**
This is a learning project for me. If you look at the code and think, *"Why on earth did he write it like this?"*—you are probably right.

If you want to fix my messy code or optimize the memory usage (please save me from the .NET overhead), feel free to open a Pull Request or an Issue.

## 📄 License
Distributed under the **MIT License**.
