# How to write Tracking Scripts

## Setup

Create a new C# script in Unity. You will get a script that looks something like this:

```csharp
using UnityEngine;

public class MyTrackingScript : MonoBehaviour
{
}
```

Now simply include the `Tracking` namespace and change the parent class from `MonoBehaviour` to `TrackingBehaviour`.
`TrackingBehaviour` is an abstract class and forces you to implement the `TrackingUpdate` method:

```csharp
using System.Collections.Generic;
using Tracking;

public class MyTrackingScript : TrackingBehaviour
{
    protected override void TrackingUpdate(HashSet<BallType> balls, CollisionEvent[] collisions, PocketEvent[] pockets)
    {
    }
}
```

This method is called once for every frame that was received from the tracking API, so you can use it like Unity's default `Update` method.

## Using `TrackingUpdate`

The `TrackingUpdate` method receives 3 parameters:

1. A `HashSet` that indicates which balls still remain on the table.
2. An array of `CollisionEvent`s, that indicates which balls collided with each other. A `CollisionEvent` contains a `BallA` and a `BallB`, which are the balls involved in the collision.
   The array is always in the order in which the collisions occur, so the first entry is always the first collision that occurred.
   The array is distinct, which means a collision between balls #1 and #3 will not appear again as another collision between #3 and #1.
   To find out if one or more balls you specify are part of the collision, use the `event.Contains` function.
3. An Array of `PocketEvent`s, that indicates which balls where pocketed. A `PocketEvent` contains the `BallType` as well as the `PocketType`.


## Code Example

```csharp
using System.Collections.Generic;
using System.Linq;
using Tracking;
using UnityEngine;

public class GameLogic : TrackingBehaviour
{
    //Called once for every frame received from the Tracking-Api
    protected override void TrackingUpdate(HashSet<BallType> balls, CollisionEvent[] collisions, PocketEvent[] pockets)
    {
        foreach (var collisionEvent in collisions)
            Debug.Log($"Collision of {collisionEvent.BallA} and {collisionEvent.BallB}");

        if (collisions.Any(x => x.Contains(BallTypes.FullBlack)))
            Debug.Log("Back 8 was hit !");

        if (collisions.Any(x => x.Contains(BallTypes.FullWhite, BallTypes.FullWhite)))
            Debug.Log("Black 8 was hit directly with White-Ball!");

        foreach (var pocketEvent in pockets)
            Debug.Log($"{pocketEvent.Ball} was pocketed in the {pocketEvent.Pocket} corner.");

        if(!balls.Contains(BallTypes.FullBlack))
            Debug.Log($"No 8-Ball in game anymore");
    }
}
```