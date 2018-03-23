# Mirror
How to do Reflection fast while keeping a simple API.

## What is this?
This little library makes it easy to get a `Func` when given only a type, methodname and optionaly some generic parameters. The main advantage of this is that its alot faster to invoke a  a `Func` than it is to use `MethodInfo.Invoke`. Some performance numbers:

<img width="536" alt="2018-03-23 21_44_08-c__windows_system32_cmd exe" src="https://user-images.githubusercontent.com/19387223/37852648-ad39a824-2ee3-11e8-94d7-9415da577c9b.png">

As you can see creating the delegate is expensive but once you have it its alot faster than `MethodInfo.Invoke` and comes within the same order of magnitude as a interface call.

## What is this not?
A complete reflection framework. Iam trying to keep this as simple as possible and purely focusing on invoking a method using reflection as fast as possible. The main reason this repo exists is for other ppl to learn how to do this but feel free to use the code. I have tried to unit test as many cases as possible to eliminate any bugs.

## Quickstart
The code that does the heavy lifting can be found [here](https://github.com/Barsonax/Mirror/blob/master/Mirror/DelegateFactory.cs). To find a example of how to use this you can check [this](https://github.com/Barsonax/Mirror/blob/master/Mirror.Example/Program.cs).
