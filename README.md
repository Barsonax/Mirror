# Mirror
How to do Reflection fast while keeping a nice API.

## What is it for?
Showing you how to get a `Action` or a `Func` when given only a type and a methodname. The main advantage of this is that its alot faster to invoke a `Action` or a `Func` than it is to use `MethodInfo.Invoke`. Its actually quite close in terms of performance to a interface call now. As a extra bonus the API is quite alot cleaner too.

## What is it not for?
Currently the main point of this repo is to get the concept accross on how to achieve this. Do not use this directly in production code. The code is currently not finished (and iam not sure if I ever will finish it) and not fully tested. Mostly leaving this here for ppl to learn from it and so that this 'trick' is easy to find in a time of need.

## Quickstart
The code that does the heavy lifting can be found [here](https://github.com/Barsonax/Mirror/blob/master/Mirror/DelegateFactory.cs). To find a example of how to use this you can check [this](https://github.com/Barsonax/Mirror/blob/master/Mirror.Example/Program.cs).
