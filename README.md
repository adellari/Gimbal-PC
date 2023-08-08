
# Runtime Gimbal PC

This project produces a gimbal tool with support for 9 DOF - Rotation (Orientation), Translation (Position), and Scale (Size).




## Demo

#### PC Build (main)
![](https://github.com/adellari/Gimbal-PC/blob/main/PC.gif)

#### iOS Build (AR Branch)
![](https://github.com/adellari/Gimbal-PC/blob/main/AR.gif)

## How it works
This sytem is composed of 3 controlling scripts: InteractionDriver.cs, InteractionAxis.cs, and InteractionObject.cs. Interaction Axis is attached to gimbal tools (ring for rotation, cone for translation, ball for scaling), InteractionDriver is attached to a camera object, and InteractionObject is attached to a GameObject which needs to be manipulated.

#### InteractionDriver.cs
Class residing on maximum 1 object in the scene to sample mouse inputs, register listener events to the appropriate gimbal tools, and invoke interaction events, passing mouse coordinates as parameters.

#### InteractionAxis.cs
Class residing on the each of the gimbal tools that compose a gimbal. Each object containing this class should be classified in its function, axis of influence, and object to influence. The function of this class is to provide gimbal tool functionality to a given object, and manipulate an InteractionObject based on inputs from an InteractionDriver

#### InteractionObject.cs 

Class residing on the object being controlled, with references to the gimbal tools that govern it. Its function is to correctly orient the gimbal tools relative to its transform.


## Roadmap

- Fix eratic and undesired rotation (glitch)

- Add momentum-based translation and rotation

- Better directional rotation (sometimes rotation is reversed)

- Publish AR branch

