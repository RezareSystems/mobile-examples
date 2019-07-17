# mobile-examples

Project to show demonstrations about how to create different functionality in mobile applications using Xamarin.

Sometimes especially in native code we might need to have code for different examples in the same files.   
Please try to either use individual functions or nicely differentiate the examples with region tags.

## Notification examples
Examples on how to create notifications with more functionality then just the basic show a message to the user.

### Notification Buttons
Examples with adding buttons to a notification to get input from the user.


## Background Audio
How to set up the different OS to play audio files while backgrounded

### Android
No aditional setup needed.   just play the file

### iOS
In the info.plist file enable audio under the "background modes".
Add the indicated lines in the AppDelegate file under the "FinishedLaunching" method
