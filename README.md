C# Screensaver scaffold
=======================

Simple Windows screensaver scaffold written in C#, based on <https://www.codeproject.com/articles/31376/%2fArticles%2f31376%2fMaking-a-C-screensaver>.
This screensaver simply displays a fullscreen form with a random line drawn at each timer's tick.

It needs to be compiled in the same architecture as the OS (in case of x64 versions of Windows untick the option `Prefer 32 bit` in the building settings of the project), renamed with a `.scr` extension and saved in `C:/Windows/System32`.

A Windows screensaver is any program able to respond to three command line flags:
- `/s`: screensaver mode, usually displays the application at full screen and closes it on any user input
- `/c`: configuration mode, usually displays a settings form
- `/p intptr`: preview mode, usually displays the application inside the handle passed as a second parameter and won't close it on user inputs.