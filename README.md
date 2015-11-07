# KUI
Flat, simple, centralized UI toolkit for WinForms

![Example Screenshot](/screenshot.png)

## How to use
1) Add a reference to KUI.dll to your WinForms project
2) Create a new windows form
3) Add a reference to KUI in your form's cs file
4) Change your form to extend "FlatWindow" instead of "Form".
5) Use the KUI controls instead of the standard WinForms ones as you build your form.

## How the theme works
All windows and control pull their font size, color, back color, and fore color from the static Theme class.
Note that the controls will completely ignore those noted properties set in the designer.
To change the color theme, you can call:

`Theme::SetFont(string fontName, int bodySize, int titleSize)`

`Theme::SetFontColor(Color c)`

`Theme::SetForeColor(Color c)`

`Theme::SetBackColor(Color c)`

`Theme::SetAccentColor(Color c)`

`Theme::SetShadowSize(int size)`

`Theme::SetShadowColor(Color c)`

respectively. Changing these values will NOT cause an automatic update/redraw of your controls. So either call these methods before initializing any part of your forms, or invalidate your forms after changing them.

## How the included windows work
Each window extends `WindowBase`, which provides the base themeing element of the form as well as the shadow effect if you're using that.

`FlatFrame` is intended to be used as a popup menu that can be docked using WinForm's standard docking feature. It provides nothing but a themed blank canvas.

`FlatToolFrame` directly extends `FlatFrame` and additionally provides a header and footer bar.

`FlatWindow` is a standalone window the provides support for resizing, dragging, and the standard Close, Maximize, Minimize buttons.

## Left to do
- Implement the rest of the most commonly used WinForms controls, such as Textbox, RichTextBox, etc.
- Have theme changes cause redraws automatically
- Have property changes cause invalidations on controls
- Implement autosizing on controls like labels
- Re-enable animations for frames after getting them to draw correctly
- Implement the standard Visual Studio designer hooks
- Implement more of the commonly used properties for the forms and controls, such as the ability to choose what window buttons are shown on the form
- More
