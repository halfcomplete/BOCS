# Setup
BOCS is implemented as a lightweight C# class library. You can integrate it into any .NET project—games, simulations, prototyping tools, or narrative engines.

## 1. Clone or Add the Library
You can include BOCS in one of two ways:
### Option A — Clone into your solution
`git clone https://github.com/halfcomplete/bocs.git`
Then add the project to your .sln file and reference it from your game or engine project.
### Option B — Add as a project reference
If you’ve downloaded the source:
`dotnet add <your-project> reference BOCS.csproj`

## 2. Project Structure
Once added, your solution should look something like:
```
/YourGame
  ├── YourGame.csproj
  └── ...
/BOCS
  ├── BOCS.csproj
  ├── /BOCSObject
  ├── /Behaviours
  ├── /Modules
  └── /EventBus
```
This structure keeps BOCS cleanly separated from game-specific logic.

## 3. Create Your First BOCSObject
```C#
var door = new BOCSObject("Door");

door.AddBehaviour(IInspectable, new InspectableBehaviour(
    description: "A wooden door with iron bands."
));

door.AddBehaviour(IInteractable, new InteractableBehaviour(
    onInteract: () => Console.WriteLine("You push the door open.")
));
```
Add as many Behaviours as needed - each one is modular and independent.

## 4. Using the EventBus
Register for and raise events:
```C#
EventBus.Subscribe("player_entered_room", e => {
    Console.WriteLine("The mask whispers as you enter...");
});

EventBus.Raise("player_entered_room", new GameEvent());
```
This lets Behaviours, masks, and systems communicate without tight coupling.

5. Requirements
* .NET 7+ recommended
* C# 10 or higher
* Runs on Windows, macOS, Linux (and WebAssembly when paired with a compatible framework such as Blazor)
