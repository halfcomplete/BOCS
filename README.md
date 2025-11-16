# BOCS
BOCS (Behaviour-based Object Composition System, pronounced “box”) is a multi-layered architectural pattern for building *interactive, reactive, and emergent game worlds*.
It is built as an *alternative to traditional entity systems* such as ECS, and is particularly suited for *narrative-driven or simulation-heavy experiences* where behaviour, context, and dynamic reactivity matter more than raw performance.
Instead of rigid inheritance hierarchies or flat component bags, BOCS objects are composed from Behaviours, and each Behaviour exposes capabilities through Modules (interfaces).
This structure enables rich, expressive object logic while keeping the engine modular, readable, and easy to extend.

## Setup
View the SETUP.md file.

## Why BOCS?
BOCS emerged from real game-development problems:
* Objects needed to respond dynamically to player decisions and world state
* Behaviours needed to interject narratively, logically, or emotionally
* ECS was too granular; inheritance was too rigid
* Adding new object types should require zero engine modification
* Complex reactive behaviour needed to stay traceable and maintainable
BOCS solves these by letting developers build objects from Behaviours that define what the object can do and how it reacts to events.

## Core Concepts
### BOCSObject*
The core entity.
A BOCSObject contains a collection of Behaviours. It holds no object-specific logic of its own; all functionality is delegated to its Behaviours.

### Behaviours
Behaviours encapsulate categories of logic. Examples:
* `InspectableBehaviour`
* `OnInteractStartDialogueBehaviour`
* `CollectibleBehaviour`
* `ConsumePowerBehaviour` (for simulations)
A Behaviour may implement multiple Modules.

### Modules
Modules are interfaces representing specific capabilities.
They describe what a Behaviour can do.
Examples:
* `IInspectable`
* `IObtainable`
* `IActOnUse`
* `IConsumer` (for simulations)
Modules allow the game engine to interact with objects generically *without knowing which Behaviour provides the capability*.

## EventBus
BOCS uses an event-driven backbone.
Objects, masks, and systems subscribe to world events and react dynamically.
This is what enables:
* Reactive narrative
* Mask interjections (for Ashborne)
* Chain-reaction world updates
* Decoupled systems

## How BOCS Works (Short Example)
```C#
// Create a new BOCSObject called "Mirror Shard"
var shard = new BOCSObject("MirrorShard");

// Add an InspectableBehaviour to the shard
shard.AddBehaviour(IInspectable, new InspectableBehaviour(
    description: "A fractured shard of silvered glass."
));

// Add a CollectibleBehaviour to the shard
shard.AddBehaviour(IObtainable, new CollectibleBehaviour(
    canPickUp: true
));

// Now this object can be inspected and collected
// The engine doesn't need to know *why* or *how*
// as the Behaviours handle everything.
```

## When to Use BOCS
BOCS is ideal for:
* Narrative-heavy games
* Simulation worlds
* Games with reactive or emergent behaviour
* Projects prioritising modularity and expressiveness
* Systems where developer clarity matters more than raw ECS performance

## When NOT to Use BOCS
* Extremely large-scale action games with thousands of entities per frame
* Games requiring ultra-low-level memory control
* Systems where performance > maintainability
