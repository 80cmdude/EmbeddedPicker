# EmbeddedPicker
Embedded page picker control for Xamarin.forms for both Android and iOS

![Image](https://media.giphy.com/media/5ryhCdUhRSRjGnaSo6/giphy.gif)

## Getting Started
This is a small sample project for xamarin.forms to show an example of an embedded picker inside a page of a xamarin forms application. Pull the project and run for either iOS or Android.

## Example

```
<local:CustomPicker ItemsSource="{Binding PickerItemSource}" SelectedIndex="{Binding PickerIndex}" SelectorLineColor="Black" />
```

## Properties

**ItemSource**  
IEnumerable type that holds the list of data to be displayed in the picker  

**SelectedIndex**  
Int type that holds the currently selected index of the picker  

**FontSize**  
Double property that sets the font size of each of the individual items in the list  

**FontFamily**  
String property that sets the font family of the individual items in the list

**SelectorLineColor** (Android Only)  
Color property that changes the color of the line on the selector. This is currently only for android as its the only platform I figured out how to do :(

**WrapSelectorWheel** (Android Only)   
Bool property that sets if a picker will scroll indefinetely. Default native option on android

**CellHeight** (iOS only)    
Float property that sets the height of each individual item in the list. This property will change depending on how big or small you choose to make your picker. This will require some trial and error to show the required amount of options :). Not needed on android as it auto sizes to fit the view.

## Contribution
My code is far from perfect so please feel free to raise pull requests or submit tickets so that I can improve if needed :)

## Acknowledgements
I originally came across this project by amay077 [link](https://github.com/amay077/Xamarin_Forms_PickerViewSample) after searching for hours. From here I have created a simpler but more customisable picker. However if you want a multiple rowed picker then the one on that link is fantastic.  

Thanks to https://github.com/djordje200179 for the UWP implementation :) 

I hope this helps :)
